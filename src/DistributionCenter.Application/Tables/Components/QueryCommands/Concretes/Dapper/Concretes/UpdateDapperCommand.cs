namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.Interfaces;
using Domain.Entities.Interfaces;
using global::Dapper;

public class UpdateDapperCommand<T>(IDbConnectionFactory<IDbConnection> dbConnectionFactory, T entity, string tableName, string fields)
    : BaseDapperCommand<T>(dbConnectionFactory, entity, tableName)
    where T : IEntity
{
    protected string Fields { get; } = fields;

    protected override async Task<Result> Execute(IDbConnection connection)
    {
        Entity.UpdatedAt = DateTime.Now;

        string query = $"UPDATE {TableName} SET {Fields} WHERE id = @Id";

        int result = await connection.ExecuteAsync(query, Entity);

        if (result <= 0)
        {
            return Error.Unexpected(
                code: "ENTITY_NOT_UPDATED",
                description: $"An error occurred while updating the {typeof(T).Name}."
            );
        }

        return Result.Ok();
    }
}
