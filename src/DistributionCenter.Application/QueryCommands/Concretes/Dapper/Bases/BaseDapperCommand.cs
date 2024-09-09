namespace DistributionCenter.Application.QueryCommands.Concretes.Dapper.Bases;

using System.Data;
using DistributionCenter.Application.Connections.Interfaces;
using DistributionCenter.Application.QueryCommands.Bases;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseDapperCommand<T>(IDbConnectionFactory dbConnectionFactory, T entity, string tableName)
    : BaseCommand<T>(entity)
    where T : IEntity
{
    protected IDbConnectionFactory DbConnectionFactory { get; } = dbConnectionFactory;
    protected string TableName { get; } = tableName;

    public override async Task<Result> ExecuteAsync()
    {
        using IDbConnection connection = await DbConnectionFactory.CreateConnectionAsync();

        Result result = await ExecuteAsync(connection);

        return result;
    }

    protected abstract Task<Result> ExecuteAsync(IDbConnection connection);
}
