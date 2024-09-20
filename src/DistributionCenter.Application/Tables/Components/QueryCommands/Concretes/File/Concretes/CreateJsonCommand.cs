namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Bases;
using Connections.File.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public class CreateJsonCommand<T>(
    IFileConnectionFactory<T> fileConnectionFactory,
    T entity
) : BaseJsonCommand<T>(fileConnectionFactory, entity)
    where T : IEntity
{
    private readonly T _entity = entity;
    private readonly IFileConnectionFactory<T> _fileConnectionFactory = fileConnectionFactory;
    protected override async Task<Result> Execute(IEnumerable<T> data)
    {
        List<T> dataList = data.ToList();
        dataList.Add(_entity);

        await _fileConnectionFactory.SaveDataAsync(dataList);
        return Result.Ok();
    }
}
