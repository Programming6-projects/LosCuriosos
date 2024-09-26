namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Bases;

using Connections.File.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Bases;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseJsonQuery<T, TResult>(IFileConnectionFactory<T> fileConnectionFactory) :
    BaseQuery<TResult>
    where T : IEntity
{
    public override async Task<Result<TResult>> ExecuteAsync()
    {
        List<T> data = await fileConnectionFactory.LoadDataAsync();
        Result<TResult> result = await Execute(data);

        if (!result.IsSuccess)
            return result.Errors;

        return result.Value;
    }

    protected abstract Task<Result<TResult>> Execute(IEnumerable<T> data);
}
