using System.Linq.Expressions;
using notification_service.domain.models.@base;
using notification_service.infrastructure.result;

namespace notification_service.infrastructure.repositories.interfaces;

public interface IDbRepository<T> : IRepository<T> where T : class, IEntity<Guid>
{
    public Task<Result<IEnumerable<T>>> GetAll(Expression<Func<T, bool>> filter);
    public Task<Result<T>> GetOne(Expression<Func<T, bool>> filter);
    public Task AddRange(IEnumerable<T> entities);
    public Task UpdateRange(IEnumerable<T> entities);
    public Task DeleteRange(IEnumerable<T> entities);

    public Task<Result<bool>> Exists(Expression<Func<T, bool>> filter);
    public Task<Result<long>> Count(Expression<Func<T, bool>> filter);
}
