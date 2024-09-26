namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Application.Tables.Connections.File.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;

public class GetAllJsonQueryTests
{
    private readonly GetAllJsonQuery<IEntity> _getAllJsonQuery;
    private readonly Mock<IFileConnectionFactory<IEntity>> _fileConnectionFactoryMock;
    private readonly Mock<IEntity> _entityMock;
    private readonly List<IEntity> _existingData;

    public GetAllJsonQueryTests()
    {
        _fileConnectionFactoryMock = new Mock<IFileConnectionFactory<IEntity>>();
        _entityMock = new Mock<IEntity>();
        _existingData = [_entityMock.Object];

        _getAllJsonQuery = new GetAllJsonQuery<IEntity>(_fileConnectionFactoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEntities_WhenEntitiesExist()
    {
        // Define Input and Output
        _ = _fileConnectionFactoryMock.Setup(static factory => factory.LoadDataAsync()).ReturnsAsync(_existingData);

        // Execute actual operation
        Result<IEnumerable<IEntity>> result = await _getAllJsonQuery.ExecuteAsync();

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(_existingData, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsError_WhenEntitiesDoNotExist()
    {
        List<IEntity> data = null!;

        // Define Input and Output
        _ = _fileConnectionFactoryMock.Setup(static factory => factory.LoadDataAsync()).ReturnsAsync(data);

        // Execute actual operation
        Result<IEnumerable<IEntity>> result = await _getAllJsonQuery.ExecuteAsync();

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, static error => error.Code == "ENTITIES_NOT_FOUND");
    }
}
