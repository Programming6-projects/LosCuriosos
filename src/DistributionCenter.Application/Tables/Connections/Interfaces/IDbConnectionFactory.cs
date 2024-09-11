namespace DistributionCenter.Application.Tables.Connections.Interfaces;

using System.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
