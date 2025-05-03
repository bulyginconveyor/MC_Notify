using MongoDB.Driver;
using static System.String;

namespace notification_service.infrastructure.repositories.mongodb;

public static class ServiceExtensionMongoDb
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECT");
        if(IsNullOrWhiteSpace(connectionString))
            throw new Exception("MONGODB_CONNECT not set! Исправляй, сука!");
        
        services.AddSingleton<IMongoClient>(s => new MongoClient(connectionString));
    }
}
