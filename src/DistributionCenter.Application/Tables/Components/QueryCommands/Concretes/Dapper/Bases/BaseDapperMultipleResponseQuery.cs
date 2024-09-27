namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Bases;

using System.Data;
using Commons.Results;
using Connections.Dapper.Interfaces;
using Domain.Entities.Interfaces;
using QueryCommands.Bases;

public abstract class BaseDapperMultipleResponseQuery<T>(IDbConnectionFactory<IDbConnection> dbConnectionFactory, string tableName) : BaseMultipleResponseQuery<T>
    where T : IEntity
{
    protected IDbConnectionFactory<IDbConnection> DbConnectionFactory { get; } = dbConnectionFactory;
    protected string TableName { get; } = tableName;

    public override async Task<Result<IEnumerable<T>>> ExecuteAsync()
    {
        using IDbConnection connection = await DbConnectionFactory.CreateConnectionAsync();

        Result<IEnumerable<T>> result = await Execute(connection);

        if (result.IsSuccess)
            return result;

        return result.Errors;
    }

    protected abstract Task<Result<IEnumerable<T>>> Execute(IDbConnection connection);
}
