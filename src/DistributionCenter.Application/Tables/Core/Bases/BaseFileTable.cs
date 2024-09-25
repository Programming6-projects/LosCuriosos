namespace DistributionCenter.Application.Tables.Core.Bases;

using Components.Information.Interfaces;
using Components.QueryCommands.Concretes.File.Concretes;
using Components.QueryCommands.Interfaces;
using Connections.File.Interfaces;
using Domain.Entities.Interfaces;
using Interfaces;

public abstract class BaseFileTable<T>(IFileConnectionFactory<T> fileConnectionFactory) : ITable<T>
    where T : IEntity
{
    protected IFileConnectionFactory<T> FileConnectionFactory { get; } = fileConnectionFactory;

    public IQuery<T> GetById(Guid id)
    {
        return new GetByIdJsonQuery<T>(FileConnectionFactory, id);
    }

    public IQuery<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public ICommand Create(T entity)
    {
        return new CreateJsonCommand<T>(FileConnectionFactory, entity);
    }

    public ICommand Update(T entity)
    {
        return new UpdateJsonCommand<T>(FileConnectionFactory, entity);
    }

    public abstract ITableInformation GetInformation();
}
