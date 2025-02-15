using Confluent.Kafka;

namespace notification_service.infrastructure.kafka;

public class EventConsumerJob(
    string serverUrl, 
    string topic, 
    AutoOffsetReset autoOffsetReset, 
    string groupId
    ) : BackgroundService
{
    private string _serverUrl = serverUrl;
    private string _topic = topic;
    private AutoOffsetReset _autoOffsetReset = autoOffsetReset;
    private string _groupId = groupId;
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _serverUrl,
            GroupId = _groupId,
            AutoOffsetReset = _autoOffsetReset
        };
        
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(_topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(5));

                if (consumeResult == null)
                    continue;
                
            }
            catch (OperationCanceledException)
            {
                //Ignore
            }                   
        }
        
        return Task.CompletedTask;
    }
}
