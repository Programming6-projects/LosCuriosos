namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.File.Interfaces;
using Domain.Entities.Interfaces;

public class UpdateJsonCommand<T>(IFileConnectionFactory<T> fileConnectionFactory,
    T entity
) : BaseJsonCommand<T>(fileConnectionFactory, entity)
    where T : IEntity
{
    private readonly T _entity = entity;
    private readonly IFileConnectionFactory<T> _fileConnectionFactory = fileConnectionFactory;

    protected override async Task<Result> Execute(IEnumerable<T> data)
    {
        List<T> dataList = data.ToList();
        int index = dataList.FindIndex(p => p.Id == _entity.Id);

        if (index == -1)
        {
            return Error.NotFound(
                code: "ENTITY_NOT_FOUND",
                description: $"The {typeof(T).Name} with ID {_entity.Id} was not found."
            );
        }

        dataList[index] = _entity;
        await _fileConnectionFactory.SaveDataAsync(dataList);
        return Result.Ok();
    }
}
