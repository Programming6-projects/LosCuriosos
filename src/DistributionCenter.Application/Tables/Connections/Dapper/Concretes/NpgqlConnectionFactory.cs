namespace DistributionCenter.Application.Tables.Connections.Dapper.Concretes;

using System.Data;
using Commons.Enums;
using Interfaces;
using Npgsql;

public class NpgqlConnectionFactory(string connectionString) : IDbConnectionFactory<IDbConnection>
{
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        NpgsqlConnection connection = new(connectionString);

        NpgsqlDataSourceBuilder dataSourceBuilder = new(connectionString);
        _ = dataSourceBuilder.MapEnum<Status>("order_status");
        _ = dataSourceBuilder.Build();

        await connection.OpenAsync();

        return connection;
    }
}
