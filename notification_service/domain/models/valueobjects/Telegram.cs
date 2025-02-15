namespace notification_service.domain.models.valueobjects;

public record Telegram
{
    public long ChatId { get; private set; }
    public long UserTgId { get; private set; }
    public string NameInTg { get; private set; }

    public static Telegram Create(long chatId, string nameInTg)
    {
        if(!DataIsValid(chatId, nameInTg))
            throw new Exception("Invalid data");
        
        return new Telegram
        {
            ChatId = chatId,
            NameInTg = nameInTg
        };
    }

    public static bool DataIsValid(long chatId, string nameInTg)
    {
        if (chatId == 0)
            return false;
        if (string.IsNullOrEmpty(nameInTg))
            return false;
        
        return true;
    }
    
}
