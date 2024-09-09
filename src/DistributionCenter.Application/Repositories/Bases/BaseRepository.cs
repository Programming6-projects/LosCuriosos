namespace DistributionCenter.Application.Repositories.Bases;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseRepository<T>(IContext context) : IRepository<T>
    where T : IEntity
{
    protected IContext Context { get; } = context;

    public virtual async Task<Result<T>> GetByIdAsync(Guid id)
    {
        Result<T> entity = await Context.SetTable<T>().GetByIdQuery(id).ExecuteAsync();

        return entity.Match(success => entity, errors => errors);
    }

    public async Task<Result<T>> CreateAsync(T entity)
    {
        Result result = await Context.SetTable<T>().CreateCommand(entity).ExecuteAsync();

        return result.Match<Result<T>>(() => entity, errors => errors);
    }

    public async Task<Result<T>> UpdateAsync(T entity)
    {
        Result result = await Context.SetTable<T>().UpdateCommand(entity).ExecuteAsync();

        return result.Match<Result<T>>(() => entity, errors => errors);
    }
}
