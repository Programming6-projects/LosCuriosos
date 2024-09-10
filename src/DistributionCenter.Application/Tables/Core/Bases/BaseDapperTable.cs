namespace DistributionCenter.Application.Tables.Core.Bases;

using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;
using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using DistributionCenter.Application.Tables.Core.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseDapperTable<T>(IDbConnectionFactory dbConnectionFactory) : ITable<T>
    where T : IEntity
{
    protected IDbConnectionFactory DbConnectionFactory { get; } = dbConnectionFactory;

    public IQuery<T> GetById(Guid id)
    {
        return new GetByIdDapperQuery<T>(
            DbConnectionFactory,
            GetInformation().TableName,
            id,
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
