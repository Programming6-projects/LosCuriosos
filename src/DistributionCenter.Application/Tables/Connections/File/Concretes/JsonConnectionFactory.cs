namespace DistributionCenter.Application.Tables.Connections.File.Concretes;

using Bases;
using Newtonsoft.Json;
using File = System.IO.File;

public class JsonConnectionFactory<T> (string tableName) : FileConnectionFactory<T>(tableName, "json")
{
    public override async Task<List<T>> LoadDataAsync()
    {
        string jsonData = await OpenFileAsync();
        return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? [];
    }

    public override async Task SaveDataAsync(IEnumerable<T> data)
    {
        List<T> existingData = await LoadDataAsync();
        existingData.AddRange(data);

        string jsonData = JsonConvert.SerializeObject(existingData, Formatting.Indented);
        await File.WriteAllTextAsync(CompletedFilePath, jsonData);
    }
}
