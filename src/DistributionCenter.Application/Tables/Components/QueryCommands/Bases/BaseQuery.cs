namespace DistributionCenter.Application.Tables.Components.QueryCommands.Bases;

using Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseQuery<T> : IQuery<T>
    where T : IEntity
{
    public abstract Task<Result<T>> ExecuteAsync();
}
