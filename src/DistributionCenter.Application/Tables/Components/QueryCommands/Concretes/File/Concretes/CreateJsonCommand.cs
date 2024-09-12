namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Bases;
using DistributionCenter.Application.Tables.Connections.Interfaces;
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
        IEnumerable<T> entityIntoList = [_entity];
        IEnumerable<T> updatedData = data.Concat(entityIntoList);

        await _fileConnectionFactory.SaveDataAsync(updatedData);
        return Result.Ok();
    }
}
