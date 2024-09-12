namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Bases;

using System.Data;
using Connections.Dapper.Interfaces;
using DistributionCenter.Application.Tables.Components.QueryCommands.Bases;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseDapperCommand<T>(IDbConnectionFactory<IDbConnection> dbConnectionFactory, T entity, string tableName)
    : BaseCommand<T>(entity)
    where T : IEntity
{
    protected IDbConnectionFactory<IDbConnection> DbConnectionFactory { get; } = dbConnectionFactory;
    protected string TableName { get; } = tableName;

    public override async Task<Result> ExecuteAsync()
    {
        using IDbConnection connection = await DbConnectionFactory.CreateConnectionAsync();

        Result result = await Execute(connection);

        return result;
    }

    protected abstract Task<Result> Execute(IDbConnection connection);
}
