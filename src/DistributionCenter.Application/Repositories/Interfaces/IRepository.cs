namespace DistributionCenter.Application.Repositories.Interfaces;

using Contexts.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public interface IRepository<T>
    where T : IEntity
{
    public IContext Context { get; }
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(T entity);
}
