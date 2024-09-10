namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using global::Dapper;
using DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Bases;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public class UpdateDapperCommand<T>(IDbConnectionFactory dbConnectionFactory, T entity, string tableName, string fields)
    : BaseDapperCommand<T>(dbConnectionFactory, entity, tableName)
    where T : IEntity
{
    protected string Fields { get; } = fields;

    protected override async Task<Result> ExecuteAsync(IDbConnection connection)
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
