using notification_service.domain.models.@base;
using notification_service.infrastructure.result;

namespace notification_service.infrastructure.repositories.interfaces;

public interface IRepository<T> where T : class, IEntity<Guid>
{
    public Task<Result<IEnumerable<T>>> GetAll();
    public Task<Result<T>> GetOne(Guid id);
    public Task<Result> Add(T entity);
    public Task<Result> Update(T entity);
    public Task<Result> Delete(Guid id);
    public Task<Result> Delete(T entity);
    public Task<Result> Save();
}
