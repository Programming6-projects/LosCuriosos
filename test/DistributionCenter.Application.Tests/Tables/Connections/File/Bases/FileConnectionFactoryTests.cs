namespace DistributionCenter.Application.Tests.Tables.Connections.File.Bases;

using System.IO;
using System.Threading.Tasks;
using DistributionCenter.Application.Tables.Connections.File.Bases;
using Domain.Entities.Interfaces;
using Xunit;
using Xunit.Abstractions;
using File = System.IO.File;

public class FileConnectionFactoryTests(ITestOutputHelper testOutputHelper)
{
    private readonly TestFileConnectionFactory _fileConnectionFactory = new(TableName, FileType);

    private const string TableName = "test";
    private const string FileType = "json";

    [Fact]
    public async Task OpenFileAsync_ReturnsFileContents_WhenFileExists()
    {
        // Define Input and Output
        string filePath = Path.Combine(Environment.CurrentDirectory, "../../../Resources/test.json");
        string expectedContent = "{\"key\":\"value\"}";
        if (!File.Exists(filePath))
        {
            string? directoryPath = Path.GetDirectoryName(filePath);

            if (directoryPath != null)
            {
                _ = Directory.CreateDirectory(directoryPath);
            }

            await File.Create(filePath).DisposeAsync();
        }
        await File.WriteAllTextAsync(filePath, expectedContent);

        // Execute actual operation
        string result = await _fileConnectionFactory.OpenFileAsync();
        testOutputHelper.WriteLine(result);

        // Verify actual result
        Assert.Equal(expectedContent, result);

        // Clean Up
        if (File.Exists(filePath)) File.Delete(filePath);
    }

    [Fact]
    public async Task LoadDataAsync_ReturnsEmptyList_WhenFileIsEmpty()
    {
        // Define Input and Output
        string filePath = Path.Combine(Environment.CurrentDirectory, "../../../Resources/test.json");
        if (File.Exists(filePath))
            File.Delete(filePath);
        await File.WriteAllTextAsync(filePath, "[]"); // Create an empty JSON array

        // Execute actual operation
        List<IEntity> result = await _fileConnectionFactory.LoadDataAsync();

        // Verify actual result
        Assert.Empty(result);
    }

    private sealed class TestFileConnectionFactory(string tableName, string fileType)
        : FileConnectionFactory<IEntity>(tableName, fileType)
    {
        public override Task<List<IEntity>> LoadDataAsync()
        {
            return Task.FromResult(new List<IEntity>());
        }

        public override Task SaveDataAsync(IEnumerable<IEntity> data)
        {
            return Task.CompletedTask;
        }
    }
}
