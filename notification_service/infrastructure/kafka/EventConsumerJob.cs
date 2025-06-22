using System.Text.Json;
using Confluent.Kafka;
using Newtonsoft.Json;
using notification_service.application.dto;
using notification_service.infrastructure.senders.email_sender;

namespace notification_service.infrastructure.kafka;

public class EventConsumerJob(
    string serverUrl, 
    string topicMain, 
    string topicSendToEmail, 
    AutoOffsetReset autoOffsetReset, 
    string groupId,
    EmailSender sender
    
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = serverUrl,
            GroupId = groupId,
            AutoOffsetReset = autoOffsetReset,
            EnableAutoCommit = false
        };
        
        using var consumerSendToEmail = new ConsumerBuilder<Ignore, string>(config).Build();
        using var consumerMain = new ConsumerBuilder<Ignore, string>(config).Build();
        
        consumerMain.Subscribe(topicMain);
        consumerSendToEmail.Subscribe(topicSendToEmail);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumerMainResult = consumerMain.Consume(TimeSpan.FromSeconds(2.5));
                var consumerSendToEmailResult = consumerSendToEmail.Consume(TimeSpan.FromSeconds(2.5));

                if (consumerMainResult == null && consumerSendToEmailResult == null)
                    continue;

                if (consumerMainResult != null)
                {
                    //TODO: Send notify-message to user

                    var res = JsonConvert.DeserializeObject<Notify>(consumerMainResult.Message.Value);
                    
                    
                    
                    continue;
                }

                if (consumerSendToEmailResult != null)
                {
                    var msg = consumerSendToEmailResult.Message.Value.Split(',');

                    if (msg.Length != 2)
                        continue; //TODO: Отработка ошибки парсинга

                    _ = await sender.SendEmailConfirmEmail(msg[0], msg[1]);
                }

            }
            catch (OperationCanceledException)
            {
                //Ignore
            }
            catch (Exception ex)
            {
                //TODO: Logging error
            }
        }
    }
}
