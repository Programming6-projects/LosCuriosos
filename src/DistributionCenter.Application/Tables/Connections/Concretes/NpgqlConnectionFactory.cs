namespace DistributionCenter.Application.Tables.Connections.Concretes;

using System.Data;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using Npgsql;

public class NpgqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        return connection;
    }
}
