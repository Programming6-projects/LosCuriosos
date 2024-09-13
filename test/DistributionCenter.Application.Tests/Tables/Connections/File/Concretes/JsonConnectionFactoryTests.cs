namespace DistributionCenter.Application.Tests.Tables.Connections.File.Concretes;

using System.Globalization;
using Application.Tables.Connections.File.Concretes;
using Domain.Entities.Concretes;
using File = System.IO.File;

public class JsonConnectionFactoryTests
{
    private readonly JsonConnectionFactory<Transport> _jsonConnectionFactory = new(TableName);
    private const string TableName = "TransportsTest";

    private static async Task FIllFileAsync()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, "../../../Resources/TransportsTest.json");

        if (!File.Exists(filePath))
        {
            string? directoryPath = Path.GetDirectoryName(filePath);

            if (directoryPath != null)
            {
                _ = Directory.CreateDirectory(directoryPath);
            }

            await File.Create(filePath).DisposeAsync();
        }

        await File.WriteAllTextAsync(filePath, "[]");

        string jsonData =
            @"
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

        await File.WriteAllTextAsync(filePath, jsonData);
    }

    [Fact]
    public async Task LoadDataAsync_ReturnsDeserializedData_WhenFileExists()
    {
        await FIllFileAsync();

        // Define Input and Output
        int expectedCount = 4;

        // Execute actual operation
        List<Transport> result = await _jsonConnectionFactory.LoadDataAsync();

        // Verify actual result
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
        Assert.Equal("Truck A", result[0].Name);
        Assert.Equal(2000, result[0].Capacity);
        Assert.Equal("Van B", result[1].Name);
        Assert.Equal(1000, result[1].Capacity);
    }

    [Fact]
    public async Task SaveDataAsync_WritesSerializedData_ToFile()
    {
        await FIllFileAsync();

        // Define Input and Output
        List<Transport> transports =
        [
            new()
            {
                Id = Guid.Parse("4a31897e-faae-49af-b54c-f9764c743e6f"),
                Name = "Truck AB",
                Capacity = 2000,
                AvailableUnits = 10,
                IsActive = true,
                CreatedAt = DateTime.Parse("2024-09-13 14:30", CultureInfo.InvariantCulture),
            },
            new()
            {
                Id = Guid.Parse("f92e3a60-f6e1-4c55-b176-e4aea14edcaf"),
                Name = "Van BC",
                Capacity = 1000,
                AvailableUnits = 5,
                IsActive = true,
                CreatedAt = DateTime.Parse("2024-09-13 14:30", CultureInfo.InvariantCulture),
            },
        ];

        // Execute actual operation
        await _jsonConnectionFactory.SaveDataAsync(transports);

        // Verify actual resul
        List<Transport> savedTransports = await _jsonConnectionFactory.LoadDataAsync();
        Assert.Equal(6, savedTransports.Count);
        Assert.Equal("Truck A", savedTransports[0].Name);
        Assert.Equal("Van BC", savedTransports[5].Name);
    }
}
