using notification_service.domain.models.@base;

namespace notification_service.domain.models.@base;

public class Entity : IEntity<Guid>
{
    public Guid Id { get; set; }
}
