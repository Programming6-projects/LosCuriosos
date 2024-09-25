namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.File.Interfaces;
using Domain.Entities.Interfaces;

public class SelectGroupJsonQuery<T>(IFileConnectionFactory<T> fileConnectionFactory,
    Func<T, bool> predicate)
    : BaseJsonMultipleResponseQuery<T>(fileConnectionFactory)
    where T : class, IEntity
{
    protected Func<T, bool> Predicate { get; } = predicate;

    protected override Result<IEnumerable<T>> Execute(IEnumerable<T> data)
    {
        List<T> enumerable = data.ToList();
        if (data == null || enumerable.Count == 0)
        {
            return Error.NotFound(code: "GROUP_NOT_FOUND", description: $"The {typeof(T).Name} group was not found.");
        }

        IEnumerable<T> filteredItems = enumerable.Where(Predicate);

        if (!filteredItems.Any())
        {
            return Error.NotFound(code: "GROUP_NOT_FOUND", description: $"No {typeof(T).Name} matched the specified condition.");
        }

        return new Result<IEnumerable<T>>(filteredItems);
    }
}
