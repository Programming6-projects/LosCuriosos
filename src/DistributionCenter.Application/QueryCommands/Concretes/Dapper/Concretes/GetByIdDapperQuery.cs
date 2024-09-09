namespace DistributionCenter.Application.QueryCommands.Concretes.Dapper.Concretes;

using System.Data;
using System.Threading.Tasks;
using global::Dapper;
using DistributionCenter.Application.Connections.Interfaces;
using DistributionCenter.Application.QueryCommands.Concretes.Dapper.Bases;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Interfaces;

public class GetByIdDapperQuery<T>(IDbConnectionFactory dbConnectionFactory, string tableName, Guid id, string fields)
    : BaseDapperQuery<T>(dbConnectionFactory, tableName)
    where T : IEntity
{
    protected Guid Id { get; } = id;
    protected string Fields { get; } = fields;

    protected override async Task<Result<T>> ExecuteAsync(IDbConnection connection)
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
