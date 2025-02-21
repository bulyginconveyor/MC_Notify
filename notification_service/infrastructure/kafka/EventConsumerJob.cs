using Confluent.Kafka;
using notification_service.services.email_sender;

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
    private string _serverUrl = serverUrl;
    private string _topic = topicMain;
    private string _topicSendToEmail = topicSendToEmail;
    private AutoOffsetReset _autoOffsetReset = autoOffsetReset;
    private string _groupId = groupId;
    private readonly EmailSender _sender = sender;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _serverUrl,
            GroupId = _groupId,
            AutoOffsetReset = _autoOffsetReset
        };
        
        using var consumerSendToEmail = new ConsumerBuilder<Ignore, string>(config).Build();
        using var consumerMain = new ConsumerBuilder<Ignore, string>(config).Build();
        
        consumerMain.Subscribe(_topic);
        consumerSendToEmail.Subscribe(_topicSendToEmail);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumerMainResult = consumerMain.Consume(TimeSpan.FromSeconds(5));
                var consumerSendToEmailResult = consumerSendToEmail.Consume(TimeSpan.FromSeconds(5));

                if (consumerMainResult == null && consumerSendToEmailResult == null)
                    continue;

                if (consumerMainResult != null)
                {
                    continue; //TODO: Send notify-message to user
                }

                if (consumerSendToEmailResult != null)
                {
                    var msg = consumerSendToEmailResult.Message.Value.Split(',');
                    
                    if (msg.Length != 2)
                        continue; //TODO: Отработка ошибки парсинга
                        
                    _ = await _sender.SendEmailConfirmEmail(msg[0], msg[1]);
                    
                    continue; 
                }

            }
            catch (OperationCanceledException)
            {
                //Ignore
            }                   
        }
    }
}
