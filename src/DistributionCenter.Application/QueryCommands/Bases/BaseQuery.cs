namespace DistributionCenter.Application.QueryCommands.Bases;

using DistributionCenter.Application.QueryCommands.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseQuery<T> : IQuery<T>
    where T : IEntity
{
    public abstract Task<Result<T>> ExecuteAsync();
}
