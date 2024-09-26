namespace DistributionCenter.Application.Tables.Components.QueryCommands.Bases;

using Interfaces;
using DistributionCenter.Commons.Results;

public abstract class BaseQuery<T> : IQuery<T>
{
    public abstract Task<Result<T>> ExecuteAsync();
}
