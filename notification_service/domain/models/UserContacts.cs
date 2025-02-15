using notification_service.domain.models.valueobjects;

namespace notification_service.domain.models;

public class UserContacts
{
    public Guid UserId { get; set; }
    public Email Email { get; set; }
    public Telegram Telegram { get; set; }
}
