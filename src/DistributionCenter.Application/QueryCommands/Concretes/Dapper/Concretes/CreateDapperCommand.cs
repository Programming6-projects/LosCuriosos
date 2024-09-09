namespace DistributionCenter.Application.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using global::Dapper;
using DistributionCenter.Application.Connections.Interfaces;
using DistributionCenter.Application.QueryCommands.Concretes.Dapper.Bases;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public class CreateDapperCommand<T>(
    IDbConnectionFactory dbConnectionFactory,
    T entity,
    string tableName,
    string fields,
    string values
) : BaseDapperCommand<T>(dbConnectionFactory, entity, tableName)
    where T : IEntity
{
    protected string Fields { get; } = fields;
    protected string Values { get; } = values;

    protected override async Task<Result> ExecuteAsync(IDbConnection connection)
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
