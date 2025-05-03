using notification_service.application.dto.enums;

namespace notification_service.application.dto;

public class Notify
{
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public TypeMessage TypeMessage { get; set; }
    public ChannelNotify? PriorityChannel { get; set; }
}
