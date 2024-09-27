namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Bases;

using Commons.Results;
using Connections.File.Interfaces;
using Domain.Entities.Interfaces;
using QueryCommands.Bases;

public abstract class BaseJsonMultipleResponseQuery<T>(IFileConnectionFactory<T> fileConnectionFactory) : BaseMultipleResponseQuery<T>
    where T : IEntity
{
    public override async Task<Result<IEnumerable<T>>> ExecuteAsync()
    {
        List<T> data = await fileConnectionFactory.LoadDataAsync();
        Result<IEnumerable<T>> result = Execute(data);

        if (!result.IsSuccess)
            return result.Errors;

        return new Result<IEnumerable<T>>(result.Value);
    }

    protected abstract Result<IEnumerable<T>> Execute(IEnumerable<T> data);
}
