namespace DistributionCenter.Application.Tables.Connections.Interfaces;

public interface IDbConnectionFactory <T>
{
    Task<T> CreateConnectionAsync();
}
