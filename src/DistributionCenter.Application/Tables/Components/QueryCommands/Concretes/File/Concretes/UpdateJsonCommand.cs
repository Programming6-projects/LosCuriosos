namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.Interfaces;
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
        T? found = data.FirstOrDefault(p => p.Id == _entity.Id);

        if (found == null)
        {
            return Error.NotFound(
                code: "ENTITY_NOT_FOUND",
                description: $"The {typeof(T).Name} with ID {_entity.Id} was not found."
            );
        }

        IEnumerable<T> updatedData = data.Select(p => p.Id == _entity.Id ? _entity : p);
        await _fileConnectionFactory.SaveDataAsync(updatedData);
        return Result.Ok();
    }
}
