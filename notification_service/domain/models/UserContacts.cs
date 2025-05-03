using MongoDB.Bson.Serialization.Attributes;
using notification_service.domain.models.@base;
using notification_service.domain.models.valueobjects;

namespace notification_service.domain.models;

public class UserContacts : Entity
{
    [BsonElement("user_id")]
    public Guid UserId { get; set; }
    
    [BsonElement("email")]
    public Email Email { get; set; }
    [BsonElement("telegram")]
    public Telegram Telegram { get; set; }
}
