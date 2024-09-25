namespace DistributionCenter.Application.Repositories.Interfaces;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public interface IRepository<T>
    where T : IEntity
{
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> CreateAsync(T entity);
    Task<Result<T>> UpdateAsync(T entity);
}
