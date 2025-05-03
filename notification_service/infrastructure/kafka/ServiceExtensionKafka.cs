using Confluent.Kafka;
using notification_service.infrastructure.senders.email_sender;
using static System.String;

namespace notification_service.infrastructure.kafka;

public static class ServiceExtensionKafka
{
    public static void AddKafkaConsumer(this IServiceCollection serviceCollection)
    {
        string? server = Environment.GetEnvironmentVariable("KAFKA_SERVER");
        string? topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC_MAIN");
        string? topicSendToEmail = Environment.GetEnvironmentVariable("KAFKA_TOPIC_SENDTOEMAIL");
        string? groupId = Environment.GetEnvironmentVariable("KAFKA_GROUP_ID");
        AutoOffsetReset autoOffsetReset;

        if (byte.TryParse(Environment.GetEnvironmentVariable("KAFKA_AUTO_OFFSET_RESET"), out byte number) && ValidEnvData([server, topic, topicSendToEmail, groupId]))
            autoOffsetReset = (AutoOffsetReset)number;
        else
            throw new Exception("KAFKA DATA in .env file don't set!");

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var sender = serviceProvider.GetService<EmailSender>();
        if (sender is null)
            throw new Exception("Email Sender don't set in DI!");
        
        serviceCollection.AddHostedService<EventConsumerJob>(e 
            => new EventConsumerJob($"{server!}", topic!, topicSendToEmail!, autoOffsetReset, groupId!, (EmailSender)sender));
    }

    private static bool ValidEnvData(string?[] strings)
    {
        foreach (var item in strings)
            if (IsNullOrEmpty(item))
                return false;
        
        return true;
    }
}
