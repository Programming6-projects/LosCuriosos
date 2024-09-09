namespace DistributionCenter.Application.Connections.Interfaces;

using System.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
