namespace DistributionCenter.Application.Tables.Connections.Concretes;

using Interfaces;
using Newtonsoft.Json;
using File = File;

public class FileConnectionFactory<T> (string tableName) : IFileConnectionFactory<T>
{
    private readonly string _completedFilePath = Path.Combine(Environment.CurrentDirectory,
        "../../../Resources/" + tableName + ".json");

    public async Task<List<T>> LoadDataAsync()
    {
        string jsonData = await OpenFileAsync();
        return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? [];
    }

    public async Task SaveDataAsync(IEnumerable<T> data)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        await File.WriteAllTextAsync(tableName, jsonData);
    }

    private async Task<string> OpenFileAsync()
    {
        if (!File.Exists(_completedFilePath))
            throw new FileNotFoundException($"We could not find the JSON file int the following route: {_completedFilePath}");

        return await File.ReadAllTextAsync(_completedFilePath);
    }
}
