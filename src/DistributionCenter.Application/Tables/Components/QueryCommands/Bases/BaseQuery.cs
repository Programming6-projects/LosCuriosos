namespace DistributionCenter.Application.Tables.Components.QueryCommands.Bases;

using DistributionCenter.Commons.Results;
using Interfaces;

public abstract class BaseQuery<T> : IQuery<T>
{
    public abstract Task<Result<T>> ExecuteAsync();
}
