namespace DistributionCenter.Application.Tables.Components.QueryCommands.Bases;

using Commons.Results;
using Domain.Entities.Interfaces;
using Interfaces;

public abstract class BaseMultipleResponseQuery<T> : IMultipleResponseQuery<T>
    where T : IEntity
{
    public abstract Task<Result<IEnumerable<T>>> ExecuteAsync();
}
