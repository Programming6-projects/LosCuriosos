namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.Dapper.Interfaces;
using Domain.Entities.Interfaces;
using global::Dapper;

public class SelectGroupDapperQuery<T>(
    IDbConnectionFactory<IDbConnection> dbConnectionFactory,
    string tableName,
    string fields,
    Func<T, bool> predicate)
    : BaseDapperMultipleResponseQuery<T>(dbConnectionFactory, tableName)
    where T : IEntity
{
    protected string Fields { get; } = fields;
    protected Func<T, bool> Predicate { get; } = predicate;

    protected override async Task<Result<IEnumerable<T>>> Execute(IDbConnection connection)
    {
        string query = $"SELECT {Fields} FROM {TableName}";

        IEnumerable<T> allItems = await connection.QueryAsync<T>(query);

        if (!allItems.Any())
        {
            return Error.NotFound(code: "GROUP_NOT_FOUND", description: $"No {typeof(T).Name} found.");
        }

        IEnumerable<T> filteredItems = allItems.Where(Predicate);

        if (!filteredItems.Any())
        {
            return Error.NotFound(code: "GROUP_NOT_FOUND", description: $"No {typeof(T).Name} matched the specified condition.");
        }

        return new Result<IEnumerable<T>>(filteredItems);
    }
}
