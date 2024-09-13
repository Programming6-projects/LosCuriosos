namespace DistributionCenter.Application.Tests.Tables.Connections.File.Concretes;

using Application.Tables.Connections.File.Concretes;
using Domain.Entities.Concretes;
using Newtonsoft.Json;
using File = System.IO.File;

public class JsonConnectionFactoryTests
{
    private readonly JsonConnectionFactory<Transport> _jsonConnectionFactory = new(TableName);
    private const string TableName = "TransportsTest";

    [Fact]
    public async Task LoadDataAsync_ReturnsDeserializedData_WhenFileExists()
    {
        // Define Input and Output
        string jsonData = @"
            [
                {
                    ""id"": ""b1a4e1f1-d0c7-49b0-9c45-3e6c9c34d375"",
                    ""name"": ""Truck A"",
                    ""capacity"": 2000,
                    ""availableUnits"": 10,
                    ""is_active"": true,
                    ""created_at"": ""2024-09-12T12:00:00Z"",
                    ""updated_at"": ""2024-09-12T12:00:00Z""
                },
                {
                    ""id"": ""ac456efb-e89b-4a3c-93f4-8bcdd49bfe56"",
                    ""name"": ""Van B"",
                    ""capacity"": 1000,
                    ""availableUnits"": 5,
                    ""is_active"": true,
                    ""created_at"": ""2024-09-12T12:00:00Z"",
                    ""updated_at"": ""2024-09-12T12:00:00Z""
                },
                {
                    ""id"": ""f8b3b85d-1b51-4d71-bafe-96cb4c61c9f7"",
                    ""name"": ""Truck C"",
                    ""capacity"": 3000,
                    ""availableUnits"": 8,
                    ""is_active"": true,
                    ""created_at"": ""2024-09-12T12:00:00Z"",
                    ""updated_at"": ""2024-09-12T12:00:00Z""
                },
                {
                    ""id"": ""3c4913a5-017b-4b7b-85a5-ea5a0e54e1c2"",
                    ""name"": ""Truck D"",
                    ""capacity"": 1500,
                    ""availableUnits"": 12,
                    ""is_active"": false,
                    ""created_at"": ""2024-09-12T12:00:00Z"",
                    ""updated_at"": ""2024-09-12T12:00:00Z""
                }
            ]";

        await File.WriteAllTextAsync(TableName, jsonData);

        // Execute actual operation
        List<Transport> result = await _jsonConnectionFactory.LoadDataAsync();

        // Verify actual result
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
        Assert.Equal("Truck A", result[0].Name);
        Assert.Equal(2000, result[0].Capacity);
        Assert.Equal("Van B", result[1].Name);
        Assert.Equal(1000, result[1].Capacity);
    }

    [Fact]
    public async Task SaveDataAsync_WritesSerializedData_ToFile()
    {
        // Define Input and Output
        List<Transport> transports =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Truck A",
                Capacity = 2000,
                AvailableUnits = 10,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Van B",
                Capacity = 1000,
                AvailableUnits = 5,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        ];

        // Execute actual operation
        await _jsonConnectionFactory.SaveDataAsync(transports);

        // Verify actual result
        Assert.True(File.Exists(TableName));
        string jsonContent = await File.ReadAllTextAsync(TableName);
        List<Transport>? savedTransports = JsonConvert.DeserializeObject<List<Transport>>(jsonContent);
        Assert.Equal(2, savedTransports?.Count);
        Assert.Equal("Truck A", savedTransports?[0].Name);
        Assert.Equal("Van B", savedTransports?[1].Name);
    }
}
