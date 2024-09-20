namespace DistributionCenter.Application.Tables.Connections.File.Interfaces;

public interface IFileConnectionFactory <T>
{
    Task<string> OpenFileAsync();
    Task<List<T>> LoadDataAsync();
    Task SaveDataAsync(IEnumerable<T> data);
}
