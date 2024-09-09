namespace DistributionCenter.Application.QueryCommands.Bases;

using DistributionCenter.Application.QueryCommands.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseCommand<T>(T entity) : ICommand
    where T : IEntity
{
    protected T Entity { get; } = entity;

    public abstract Task<Result> ExecuteAsync();
}
