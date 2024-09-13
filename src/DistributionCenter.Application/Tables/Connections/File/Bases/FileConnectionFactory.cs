namespace DistributionCenter.Application.Tables.Connections.File.Bases;

using Interfaces;
using File = System.IO.File;

public abstract class FileConnectionFactory<T>(string tableName, string fileType) : IFileConnectionFactory<T>
{
    private readonly string _completedFilePath = Path.Combine(Environment.CurrentDirectory,
        "../../../Resources/" + tableName + "." + fileType);

    protected string CompletedFilePath => _completedFilePath;

    public async Task<string> OpenFileAsync()
    {
        if (!File.Exists(_completedFilePath))
        {
            string? directoryPath = Path.GetDirectoryName(_completedFilePath);

            if (directoryPath != null)
            {
                _ = Directory.CreateDirectory(directoryPath);
            }

            await File.Create(_completedFilePath).DisposeAsync();
            await File.WriteAllTextAsync(_completedFilePath, "[]");
        }
        return await File.ReadAllTextAsync(_completedFilePath);
    }

    public abstract Task<List<T>> LoadDataAsync();
    public abstract Task SaveDataAsync(IEnumerable<T> data);
}
