namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Bases;

using DistributionCenter.Application.Tables.Components.QueryCommands.Bases;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseJsonQuery<T>(IFileConnectionFactory<T> fileConnectionFactory) :
    BaseQuery<T>
    where T : IEntity
{
    public override async Task<Result<T>> ExecuteAsync()
    {
        List<T> data = await fileConnectionFactory.LoadDataAsync();
        Result<T> result = await Execute(data);

        if (!result.IsSuccess)
            return result.Errors;

        return result.Value;
    }

    protected abstract Task<Result<T>> Execute(IEnumerable<T> data);
}
