using MongoDB.Bson.Serialization.Attributes;

namespace notification_service.domain.models.valueobjects;

public record Telegram
{
    [BsonElement("chat_id")]
    public long ChatId { get; private set; }
    [BsonElement("user_id")]
    public long UserId { get; private set; }
    [BsonElement("name")]
    public string NameUser { get; private set; }

    public static Telegram Create(long chatId, long userId, string nameInTg)
    {
        if(!DataIsValid(chatId, userId, nameInTg))
            throw new Exception("Invalid data");
        
        return new Telegram
        {
            ChatId = chatId,
            NameUser = nameInTg,
            UserId = userId,
        };
    }

    public static bool DataIsValid(long chatId, long userId, string nameInTg)
    {
        if (chatId == 0 || userId == 0)
            return false;
        if (string.IsNullOrEmpty(nameInTg))
            return false;
        
        return true;
    }
    
}
