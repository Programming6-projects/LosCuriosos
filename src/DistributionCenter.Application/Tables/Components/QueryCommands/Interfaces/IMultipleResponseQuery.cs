namespace DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;

using Commons.Results;
using Domain.Entities.Interfaces;

public interface IMultipleResponseQuery<T> where T : IEntity
{
    Task<Result<IEnumerable<T>>> ExecuteAsync();
}
