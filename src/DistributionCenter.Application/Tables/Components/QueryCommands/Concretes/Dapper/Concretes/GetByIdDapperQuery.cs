namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.Dapper.Interfaces;
using Domain.Entities.Interfaces;
using global::Dapper;

public class GetByIdDapperQuery<T>(IDbConnectionFactory<IDbConnection> dbConnectionFactory, string tableName, Guid id, string fields)
    : BaseDapperQuery<T>(dbConnectionFactory, tableName)
    where T : IEntity
{
    protected Guid Id { get; } = id;
    protected string Fields { get; } = fields;

    protected override async Task<Result<T>> Execute(IDbConnection connection)
    {
        string query = $"SELECT {Fields} FROM {TableName} WHERE id = @Id";

        T? entity = await connection.QueryFirstOrDefaultAsync<T>(query, new { Id });

        if (entity is null)
        {
            return Error.NotFound(code: "ENTITY_NOT_FOUND", description: $"The {typeof(T).Name} was not found.");
        }

        return entity;
    }
}
