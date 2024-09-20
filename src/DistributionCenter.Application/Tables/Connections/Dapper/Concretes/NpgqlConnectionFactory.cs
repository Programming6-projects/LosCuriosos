namespace DistributionCenter.Application.Tables.Connections.Dapper.Concretes;

using System.Data;
using Interfaces;
using Npgsql;

public class NpgqlConnectionFactory(string connectionString) : IDbConnectionFactory<IDbConnection>
{
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        NpgsqlConnection connection = new(connectionString);

        await connection.OpenAsync();

        return connection;
    }
}
