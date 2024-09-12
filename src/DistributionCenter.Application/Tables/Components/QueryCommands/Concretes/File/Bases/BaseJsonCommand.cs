namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Bases;

using Connections.File.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Bases;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseJsonCommand<T>(IFileConnectionFactory<T> fileConnectionFactory, T entity)
    : BaseCommand<T>(entity)
    where T : IEntity
{
    public override async Task<Result> ExecuteAsync()
    {
        List<T> data = await fileConnectionFactory.LoadDataAsync();
        Result result = await Execute(data);

        return result;
    }

    protected abstract Task<Result> Execute(IEnumerable<T> data);
}
