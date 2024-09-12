namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.Dapper.Interfaces;
using Domain.Entities.Interfaces;
using global::Dapper;

public class CreateDapperCommand<T>(
    IDbConnectionFactory<IDbConnection> dbConnectionFactory,
    T entity,
    string tableName,
    string fields,
    string values
) : BaseDapperCommand<T>(dbConnectionFactory, entity, tableName)
    where T : IEntity
{
    protected string Fields { get; } = fields;
    protected string Values { get; } = values;

    protected override async Task<Result> Execute(IDbConnection connection)
    {
        string query = $"INSERT INTO {TableName} ({Fields}) VALUES ({Values})";

        int result = await connection.ExecuteAsync(query, Entity);

        if (result == 0)
        {
            return Error.Unexpected(
                code: "ENTITY_NOT_CREATED",
                description: $"An error occurred while creating the {typeof(T).Name}."
            );
        }

        return Result.Ok();
    }
}
