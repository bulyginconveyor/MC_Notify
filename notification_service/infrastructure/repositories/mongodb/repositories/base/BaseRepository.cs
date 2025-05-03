using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using notification_service.domain.models.@base;
using notification_service.infrastructure.repositories.interfaces;
using notification_service.infrastructure.result;
using notification_service.infrastructure.result.errors;
using notification_service.infrastructure.string_extensions;

namespace notification_service.infrastructure.repositories.mongodb.repositories.@base;

public class BaseReposit1ory<T>(IMongoClient mongoCli) : IDbRepository<T> where T : class, IEntity<Guid>
{
    public IMongoDatabase _context = mongoCli.GetDatabase(
        Environment.GetEnvironmentVariable("MONGODB_NAME") 
        ?? throw new Exception("MONGODB_NAME not set! Исправляй с"));
    
    private string collectionName = typeof(T).Name.Pluralize().ToSnakeCase();
    
    //TODO: Добавить проверку на существование коллекции
    
    public async Task<Result<IEnumerable<T>>> GetAll()
    {
        //TODO: Добавить отлов ошибок
        
        var list = _context.GetCollection<T>(collectionName);
        var res = await list.Find(_ => true).ToListAsync();

        return !res.Any() 
            ? Result<IEnumerable<T>>.Failure(Errors.Repository.NotFound) 
            : Result<IEnumerable<T>>.Success(res);
    }
    public Task<Result<IEnumerable<T>>> GetAll(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<T>> GetOne(Guid id)
    {
        //TODO: Добавить отлов ошибок
        
        var list = _context.GetCollection<T>(collectionName);
        var res = await list.Find(e => e.UserId == id).FirstOrDefaultAsync();
        
        return res is null 
            ? Result<T>.Failure(Errors.Repository.NotFound) 
            : Result<T>.Success(res);
    }
    public Task<Result<T>> GetOne(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Add(T entity)
    {
        //TODO: Добавить отлов ошибок
        
        var list = _context.GetCollection<T>(collectionName);
        await list.InsertOneAsync(entity);
        
        return Result.Success();
    }

    public async Task<Result> Update(T entity)
    {
        //TODO: Добавить отлов ошибок
        
        var list = _context.GetCollection<T>(collectionName);
        await list.ReplaceOneAsync(e => e.UserId == entity.UserId, entity);
        
        return Result.Success();
    }

    public async Task<Result> Delete(Guid id)
    {
        //TODO: Добавить отлов ошибок
        
        var list = _context.GetCollection<T>(collectionName);
        await list.DeleteOneAsync(e => e.UserId == id);
        
        return Result.Success();
    }

    public async Task<Result> Delete(T entity)
    {
        //TODO: Добавить отлов ошибок
        
        var list = _context.GetCollection<T>(collectionName);
        await list.DeleteOneAsync(e => e.UserId == entity.UserId);
        
        return Result.Success();
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        //TODO: Добавить отлов ошибок
        var list = _context.GetCollection<T>(collectionName);
        await list.InsertManyAsync(entities);
    }
    public async Task UpdateRange(IEnumerable<T> entities)
    {
        //TODO: Добавить отлов ошибок
        var list = _context.GetCollection<T>(collectionName);
        
        var tasks = new List<Task>();
        foreach (var entity in entities)
            tasks.Add(list.ReplaceOneAsync(e => e.UserId == entity.UserId, entity));
        
        await Task.WhenAll(tasks);
    }
    public Task DeleteRange(IEnumerable<T> entities)
    {
        //TODO: Добавить отлов ошибок
        var list = _context.GetCollection<T>(collectionName);
        
        var tasks = new List<Task>();
        foreach (var entity in entities)
            tasks.Add(list.DeleteOneAsync(e => e.UserId == entity.UserId));
        
        return Task.WhenAll(tasks);
    }

    public async Task<Result<bool>> Exists(Expression<Func<T, bool>> filter)
    {
        //TODO: Добавить отлов ошибок
        var list = _context.GetCollection<T>(collectionName);
        var res = await (await list.FindAsync(filter)).AnyAsync();
        
        return Result<bool>.Success(res);
    }

    public async Task<Result<long>> Count(Expression<Func<T, bool>> filter)
    {
        //TODO: Добавить отлов ошибок
        var list = _context.GetCollection<T>(collectionName);
        var res = await list.CountDocumentsAsync(filter);
        
        return Result<long>.Success(res);
    }
    
    public Task<Result> Save() => new(() => { return Result.Success(); });
}
