namespace notification_service.domain.models.@base;

public interface IEntity<T>
{
    public T Id { get; set; }
}
