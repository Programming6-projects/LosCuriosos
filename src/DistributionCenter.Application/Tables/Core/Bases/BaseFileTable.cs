namespace DistributionCenter.Application.Tables.Core.Bases;

using Components.Information.Interfaces;
using Components.QueryCommands.Concretes.File.Concretes;
using Components.QueryCommands.Interfaces;
using Connections.File.Interfaces;
using Domain.Entities.Interfaces;
using Interfaces;

public abstract class BaseFileTable<T>(IFileConnectionFactory<T> fileConnectionFactory)
    : ITable<T>
    where T : class, IEntity
{
    protected IFileConnectionFactory<T> FileConnectionFactory { get; } = fileConnectionFactory;

    public IQuery<T> GetById(Guid id)
    {
        return new GetByIdJsonQuery<T>(FileConnectionFactory, id);
    }

    public IMultipleResponseQuery<T> SelectWhere(Func<T, bool> predicate)
    {
        return new SelectGroupJsonQuery<T>(FileConnectionFactory, predicate);
    }

    public ICommand Create(T entity)
    {
        return new CreateJsonCommand<T>(
            FileConnectionFactory,
            entity);
    }

    public ICommand Update(T entity)
    {
        return new UpdateJsonCommand<T>(
            FileConnectionFactory,
            entity);
    }

    public abstract ITableInformation GetInformation();
}

