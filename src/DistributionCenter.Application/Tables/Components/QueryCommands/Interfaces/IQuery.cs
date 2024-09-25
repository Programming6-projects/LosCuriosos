namespace DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;

using DistributionCenter.Commons.Results;

public interface IQuery<T>
{
    Task<Result<T>> ExecuteAsync();
}
