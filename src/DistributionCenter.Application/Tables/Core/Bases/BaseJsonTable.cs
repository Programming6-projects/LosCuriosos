namespace DistributionCenter.Application.Tables.Core.Bases;

using Components.Information.Interfaces;
using Components.QueryCommands.Concretes.File.Concretes;
using Components.QueryCommands.Interfaces;
using Connections.Interfaces;
using Domain.Entities.Interfaces;
using Interfaces;

public abstract class BaseJsonTable<T>(IFileConnectionFactory<T> fileConnectionFactory) : ITable<T>
    where T : IEntity
{
    protected IFileConnectionFactory<T> FileConnectionFactory { get; } = fileConnectionFactory;

    public IQuery<T> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public ICommand Create(T entity)
    {
        return new CreateJsonCommand<T>(
            FileConnectionFactory,
            entity);
    }

    public ICommand Update(T entity)
    {
        throw new NotImplementedException();
    }

    public abstract ITableInformation GetInformation();
}

