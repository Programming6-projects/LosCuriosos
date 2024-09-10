namespace DistributionCenter.Application.Tables.Components.QueryCommands.Bases;

using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseCommand<T>(T entity) : ICommand
    where T : IEntity
{
    protected T Entity { get; } = entity;

    public abstract Task<Result> ExecuteAsync();
}
