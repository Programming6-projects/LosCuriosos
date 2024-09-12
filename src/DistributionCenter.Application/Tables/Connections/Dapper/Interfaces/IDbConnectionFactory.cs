namespace DistributionCenter.Application.Tables.Connections.Dapper.Interfaces;

public interface IDbConnectionFactory <T>
{
    Task<T> CreateConnectionAsync();
}
