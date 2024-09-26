namespace DistributionCenter.Application.Tables.Components.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using global::Dapper;
using Bases;
using Commons.Errors;
using Commons.Results;
using Connections.Dapper.Interfaces;
using Domain.Entities.Interfaces;

public class GetAllDapperQuery<T>(
    IDbConnectionFactory<IDbConnection> dbConnectionFactory,
    string tableName,
    string fields
) : BaseDapperQuery<IEnumerable<T>>(dbConnectionFactory, tableName)
    where T : IEntity
{
    protected string Fields { get; } = fields;

    protected override async Task<Result<IEnumerable<T>>> Execute(IDbConnection connection)
    {
        string query = $"SELECT {Fields} FROM {TableName}";

        IEnumerable<T>? entities = await connection.QueryAsync<T>(query);

        if (entities is null)
        {
            return Error.NotFound(
                code: "ENTITIES_NOT_FOUND",
                description: $"The {typeof(T).Name} entities were not found."
            );
        }

        return entities.ToList();
    }
}
