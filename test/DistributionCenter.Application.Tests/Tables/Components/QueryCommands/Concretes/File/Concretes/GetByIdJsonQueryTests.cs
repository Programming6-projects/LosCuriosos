namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Application.Tables.Connections.File.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;

public class GetByIdJsonQueryTests
{
    private readonly GetByIdJsonQuery<IEntity> _getByIdJsonQuery;
    private readonly Mock<IFileConnectionFactory<IEntity>> _fileConnectionFactoryMock;
    private readonly Mock<IEntity> _entityMock;
    private readonly Guid _existingId;
    private readonly List<IEntity> _existingData;

    public GetByIdJsonQueryTests()
    {
        _existingId = Guid.NewGuid();
        _fileConnectionFactoryMock = new Mock<IFileConnectionFactory<IEntity>>();
        _entityMock = new Mock<IEntity>();
        _ = _entityMock.SetupGet(e => e.Id).Returns(_existingId);

        _existingData = [_entityMock.Object];

        _getByIdJsonQuery = new GetByIdJsonQuery<IEntity>(
            _fileConnectionFactoryMock.Object,
            _existingId
        );
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEntity_WhenEntityExists()
    {
        // Define Input and Output
        _ = _fileConnectionFactoryMock
            .Setup(factory => factory.LoadDataAsync())
            .ReturnsAsync(_existingData);

        // Execute actual operation
        Result<IEntity> result = await _getByIdJsonQuery.ExecuteAsync();

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(_entityMock.Object, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsError_WhenEntityDoesNotExist()
    {
        // Define Input and Output
        Guid nonExistentId = Guid.NewGuid(); // ID that doesn't exist in the data
        GetByIdJsonQuery<IEntity> query = new(_fileConnectionFactoryMock.Object, nonExistentId);
        _ = _fileConnectionFactoryMock
            .Setup(factory => factory.LoadDataAsync())
            .ReturnsAsync(_existingData);

        // Execute actual operation
        Result<IEntity> result = await query.ExecuteAsync();

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "ENTITY_NOT_FOUND");
    }
}
