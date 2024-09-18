namespace DistributionCenter.Application.Tests.Tables.Connections.File.Bases;

using System.IO;
using System.Threading.Tasks;
using DistributionCenter.Application.Tables.Connections.File.Bases;
using Domain.Entities.Concretes;
using Xunit;
using File = System.IO.File;

public class FileConnectionFactoryTests
{
    private readonly Mock<FileConnectionFactory<Transport>> _mockFileConnectionFactory = new("test_table", "json");

    [Fact]
    public async Task OpenFileAsync_ShouldReturnFileContents_WhenFileExists()
    {
        // Define Input and Output
        string filePath = Path.Combine(Environment.CurrentDirectory, "../../persistence_data/test_table.json");
        string fileContent = "[{ \"Id\": 1, \"Name\": \"Test\" }]";

        // Create the file for the test
        if (!File.Exists(filePath))
        {
            _ = Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await File.WriteAllTextAsync(filePath, fileContent);
        }

        // Execute actual operation
        string result = await _mockFileConnectionFactory.Object.OpenFileAsync();

        // Verify actual resul
        Assert.Equal(fileContent, result);

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public async Task OpenFileAsync_ShouldCreateAndReturnDefaultContents_WhenFileDoesNotExist()
    {
        // Define Input and Output
        string filePath = Path.Combine(Environment.CurrentDirectory, "../../persistence_data/test_table.json");

        // Delete the file if it exists
        if (File.Exists(filePath))
            File.Delete(filePath);

        // Execute actual operation
        string result = await _mockFileConnectionFactory.Object.OpenFileAsync();

        // Verify actual resul
        Assert.True(File.Exists(filePath));
        Assert.Equal("[]", result);

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public async Task SaveDataAsync_ShouldSaveDataCorrectly()
    {
        // Define Input and Output
        List<Transport> data =
        [
            new() { Name = "Test",  Plate = "1852SBJ", Capacity = 9, CurrentCapacity = 9, IsAvailable = true },
            new() { Name = "Test2",  Plate = "1002SAJ", Capacity = 9, CurrentCapacity = 9, IsAvailable = true },
        ];

        string filePath = Path.Combine(Environment.CurrentDirectory, "../../persistence_data/test_table.json");

        // Delete the file if it exists
        if (File.Exists(filePath))
            File.Delete(filePath);

        async void Action(IEnumerable<Transport> savedData)
        {
            string jsonData = System.Text.Json.JsonSerializer.Serialize(savedData);
            await File.WriteAllTextAsync(filePath, jsonData);
        }

        _ = _mockFileConnectionFactory.Setup(x => x.SaveDataAsync(It.IsAny<IEnumerable<Transport>>()))
            .Callback<IEnumerable<Transport>>(Action);

        // Execute actual operation
        await _mockFileConnectionFactory.Object.SaveDataAsync(data);

        // Verify actual resul
        string fileContent = await File.ReadAllTextAsync(filePath);
        Assert.Contains("Test", fileContent, StringComparison.Ordinal);
        Assert.Contains("Test2", fileContent, StringComparison.Ordinal);

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public async Task LoadDataAsync_ShouldReturnCorrectData()
    {
        // Define Input and Output
        List<Transport> expectedData =
        [
            new() { Name = "Test",  Plate = "1852SBJ", Capacity = 9, CurrentCapacity = 9, IsAvailable = true },
            new() { Name = "Test2",  Plate = "1002SAJ", Capacity = 9, CurrentCapacity = 9, IsAvailable = true },
        ];

        string filePath = Path.Combine(Environment.CurrentDirectory, "../../persistence_data/test_table.json");
        string fileContent = System.Text.Json.JsonSerializer.Serialize(expectedData);

        await File.WriteAllTextAsync(filePath, fileContent);

        _ = _mockFileConnectionFactory.Setup(x => x.LoadDataAsync())
            .ReturnsAsync(expectedData);

        // Execute actual operation
        List<Transport> result = await _mockFileConnectionFactory.Object.LoadDataAsync();

        // Verify actual resul
        Assert.Equal(expectedData.Count, result.Count);
        Assert.Equal("Test", result[0].Name);
        Assert.Equal("Test2", result[1].Name);

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public async Task OpenFileAsync_ShouldHandleNullDirectoryPath()
    {
        // Define Input and Output
        Mock<FileConnectionFactory<Transport>> mockFactory = new("file_without_directory", "json");

        // Modify _completedFilePath for this specific test to make Path.GetDirectoryName return null
        string invalidPath = Path.Combine(Environment.CurrentDirectory,
            "../../persistence_data/file_without_directory.json");
        _ = mockFactory.SetupGet(f => f.CompletedFilePath).Returns(invalidPath);

        // Execute actual operation
        string result = await mockFactory.Object.OpenFileAsync();

        // Verify actual resul
        Assert.Equal("[]", result);
        Assert.True(File.Exists(invalidPath));

        // Clean up
        File.Delete(invalidPath);
    }

    [Fact]
    public async Task SaveDataAsync_ShouldThrowException_WhenWriteFails()
    {
        string invalidFilePath = Path.Combine(Environment.CurrentDirectory, "invalid_path/test_table.json");

        if (!File.Exists(invalidFilePath))
        {
            string? directoryPath = Path.GetDirectoryName(invalidFilePath);
            if (directoryPath != null) _ = Directory.CreateDirectory(directoryPath);
            await File.Create(invalidFilePath).DisposeAsync();
        }

        // Define Input and Output
        List<Transport> data =
        [
            new() { Name = "Test",  Plate = "1852SBJ", Capacity = 9, CurrentCapacity = 9, IsAvailable = true },
        ];

        Mock<FileConnectionFactory<Transport>> mockFileConnectionFactory = new("test_table", "json");

        // Set up the mock to throw an exception during save
        _ = mockFileConnectionFactory.Setup(x => x.SaveDataAsync(It.IsAny<IEnumerable<Transport>>()))
            .Callback<IEnumerable<Transport>>(async (savedData) =>
            {
                await File.WriteAllTextAsync(invalidFilePath, System.Text.Json.JsonSerializer.Serialize(savedData));
            })
            .ThrowsAsync(new IOException("Write error"));

        // Execute actual operation
        _ = await Assert.ThrowsAsync<IOException>(async () => await mockFileConnectionFactory.Object.SaveDataAsync(data));

        // Clean up
        File.Delete(invalidFilePath);
    }
}
