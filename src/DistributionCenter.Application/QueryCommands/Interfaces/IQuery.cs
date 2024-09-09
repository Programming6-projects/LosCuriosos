namespace DistributionCenter.Application.QueryCommands.Interfaces;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public interface IQuery<T>
    where T : IEntity
{
    Task<Result<T>> ExecuteAsync();
}
