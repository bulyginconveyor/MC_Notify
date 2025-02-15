using Confluent.Kafka;

namespace notification_service.infrastructure.kafka;

public static class ServiceExtensionKafka
{
    public static void AddKafkaConsumer(this IServiceCollection serviceCollection)
    {
        string serverUrl = Environment.GetEnvironmentVariable("KAFKA_URL");
        string topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
        string groupId = Environment.GetEnvironmentVariable("KAFKA_GROUP_ID");
        AutoOffsetReset autoOffsetReset;

        if (byte.TryParse(Environment.GetEnvironmentVariable("KAFKA_AUTO_OFFSET_RESET"), out byte number))
            autoOffsetReset = (AutoOffsetReset)number;
        else
            throw new Exception("KAFKA_AUTO_OFFSET_RESET in .env file don't set! Исправляй, сука!");    
        
        serviceCollection.AddHostedService<EventConsumerJob>(e 
            => new EventConsumerJob(serverUrl, topic, autoOffsetReset, groupId));
    }
}
