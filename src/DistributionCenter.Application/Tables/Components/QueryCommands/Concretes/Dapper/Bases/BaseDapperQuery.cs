namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Bases;

using System.Data;
using DistributionCenter.Application.Tables.Components.QueryCommands.Bases;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseDapperQuery<T>(IDbConnectionFactory dbConnectionFactory, string tableName) : BaseQuery<T>
    where T : IEntity
{
    protected IDbConnectionFactory DbConnectionFactory { get; } = dbConnectionFactory;
    protected string TableName { get; } = tableName;

    public override async Task<Result<T>> ExecuteAsync()
    {
        using IDbConnection connection = await DbConnectionFactory.CreateConnectionAsync();

        Result<T> result = await ExecuteAsync(connection);

        if (result.IsSuccess)
        {
            return result.Value;
        }

        return result.Errors;
    }

    protected abstract Task<Result<T>> ExecuteAsync(IDbConnection connection);
}
