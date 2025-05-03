namespace notification_service.domain.models.@base;

public interface IEntity<T>
{
    public T UserId { get; set; }
}
