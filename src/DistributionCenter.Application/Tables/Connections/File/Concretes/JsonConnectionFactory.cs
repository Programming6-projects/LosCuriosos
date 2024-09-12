namespace DistributionCenter.Application.Tables.Connections.File.Concretes;

using Bases;
using Newtonsoft.Json;
using File = System.IO.File;

public class JsonConnectionFactory<T> (string tableName) : FileConnectionFactory<T>(tableName, "json")
{
    private readonly string _tableName = tableName;

    public override async Task<List<T>> LoadDataAsync()
    {
        string jsonData = await OpenFileAsync();
        return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? [];
    }

    public override async Task SaveDataAsync(IEnumerable<T> data)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        await File.WriteAllTextAsync(_tableName, jsonData);
    }
}
