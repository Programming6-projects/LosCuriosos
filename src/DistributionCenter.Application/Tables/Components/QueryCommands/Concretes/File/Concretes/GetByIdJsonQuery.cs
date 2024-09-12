namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.Interfaces;
using Domain.Entities.Interfaces;

public class GetByIdJsonQuery<T>(IFileConnectionFactory<T> fileConnectionFactory, Guid id)
    : BaseJsonQuery<T>(fileConnectionFactory)
    where T : IEntity
{
    private Guid Id { get; } = id;

    protected override Task<Result<T>> Execute(IEnumerable<T> data)
    {
        T? entity = data.FirstOrDefault(p => p.Id == Id);

        if (entity is null)
        {
            return Task.FromResult<Result<T>>(Error.NotFound(
                code: "ENTITY_NOT_FOUND",
                description: $"The {typeof(T).Name} was not found."));
        }

        return Task.FromResult<Result<T>>(entity);
    }
}
