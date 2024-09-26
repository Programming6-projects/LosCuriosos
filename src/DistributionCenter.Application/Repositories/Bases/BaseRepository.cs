namespace DistributionCenter.Application.Repositories.Bases;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseRepository<T>(IContext context) : IRepository<T>
    where T : IEntity
{
    public IContext Context { get; } = context;

    public virtual async Task<Result<T>> GetByIdAsync(Guid id)
    {
        Result<T> entity = await Context.SetTable<T>().GetById(id).ExecuteAsync();

        return entity.Match(success => entity, errors => errors);
    }

    public async Task<Result<T>> CreateAsync(T entity)
    {
        Result result = await Context.SetTable<T>().Create(entity).ExecuteAsync();

        return result.Match<Result<T>>(() => entity, errors => errors);
    }

    public async Task<Result<int>> CreateAllAsync(IEnumerable<T> entities)
    {
        int rowsAffected = 0;
        foreach (T entity in entities)
        {
            Result result = await CreateAsync(entity);
            if (result.IsSuccess)
            {
                rowsAffected++;
            }
        }

        return rowsAffected;
    }

    public async Task<Result<T>> UpdateAsync(T entity)
    {
        Result result = await Context.SetTable<T>().Update(entity).ExecuteAsync();

        return result.Match<Result<T>>(() => entity, errors => errors);
    }

    public async Task<Result<int>> UpdateAllAsync(IEnumerable<T> entities)
    {
        int rowsAffected = 0;
        foreach (T entity in entities)
        {
            Result result = await UpdateAsync(entity);
            if (result.IsSuccess)
            {
                rowsAffected++;
            }
        }

        return rowsAffected;
    }

    public async Task<Result<IEnumerable<T>>> SelectWhereAsync(Func<T, bool> predicate)
    {
        Result<IEnumerable<T>> result = await Context.SetTable<T>().SelectWhere(predicate).ExecuteAsync();

        return result.Match<Result<IEnumerable<T>>>(() => result, errors => errors);
    }
}
