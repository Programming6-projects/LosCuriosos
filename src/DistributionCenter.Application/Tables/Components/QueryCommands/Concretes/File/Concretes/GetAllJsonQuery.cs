namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.File.Interfaces;
using Domain.Entities.Interfaces;

public class GetAllJsonQuery<T>(IFileConnectionFactory<T> fileConnectionFactory)
    : BaseJsonQuery<T, IEnumerable<T>>(fileConnectionFactory)
    where T : IEntity
{
    protected override Task<Result<IEnumerable<T>>> Execute(IEnumerable<T> data)
    {
        if (data is null)
        {
            return Task.FromResult<Result<IEnumerable<T>>>(
                Error.NotFound(code: "ENTITIES_NOT_FOUND", description: $"The {typeof(T).Name} entities were not found")
            );
        }

        return Task.FromResult<Result<IEnumerable<T>>>(data.ToList());
    }
}
