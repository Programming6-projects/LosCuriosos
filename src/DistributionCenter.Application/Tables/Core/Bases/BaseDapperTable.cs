namespace DistributionCenter.Application.Tables.Core.Bases;

using System.Data;
using Connections.Dapper.Interfaces;
using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;
using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;
using Interfaces;

public abstract class BaseDapperTable<T>(IDbConnectionFactory<IDbConnection> dbConnectionFactory) : ITable<T>
    where T : IEntity
{
    protected IDbConnectionFactory<IDbConnection> DbConnectionFactory { get; } = dbConnectionFactory;

    public IQuery<T> GetById(Guid id)
    {
        return new GetByIdDapperQuery<T>(
            DbConnectionFactory,
            GetInformation().TableName,
            id,
            GetInformation().GetByIdFields
        );
    }

    public IQuery<IEnumerable<T>> GetAll()
    {
        return new GetAllDapperQuery<T>(
            DbConnectionFactory,
            GetInformation().TableName,
            GetInformation().GetByIdFields
        );
    }

    public ICommand Create(T entity)
    {
        return new CreateDapperCommand<T>(
            DbConnectionFactory,
            entity,
            GetInformation().TableName,
            GetInformation().CreateFields,
            GetInformation().CreateValues
        );
    }

    public ICommand Update(T entity)
    {
        return new UpdateDapperCommand<T>(
            DbConnectionFactory,
            entity,
            GetInformation().TableName,
            GetInformation().UpdateFields
        );
    }

    public abstract ITableInformation GetInformation();
}
