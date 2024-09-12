namespace DistributionCenter.Application.Tables.Connections.Interfaces;

public interface IFileConnectionFactory <T>
{
    Task<List<T>> LoadDataAsync();
    Task SaveDataAsync(IEnumerable<T> data);
}
