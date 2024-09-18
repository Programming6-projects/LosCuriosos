namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Application.Tables.Components.QueryCommands.Concretes.File.Concretes;
using Application.Tables.Connections.File.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;

public class UpdateJsonCommandTests
{
    private readonly UpdateJsonCommand<IEntity> _updateJsonCommand;
    private readonly Mock<IFileConnectionFactory<IEntity>> _fileConnectionFactoryMock;
    private readonly Mock<IEntity> _entityMock;
    private readonly List<IEntity> _existingData;

    public UpdateJsonCommandTests()
    {
        Guid existingId = Guid.NewGuid();
        _fileConnectionFactoryMock = new Mock<IFileConnectionFactory<IEntity>>();
        _entityMock = new Mock<IEntity>();
        _ = _entityMock.SetupGet(e => e.Id).Returns(existingId);

        _existingData = [_entityMock.Object];

        _updateJsonCommand = new UpdateJsonCommand<IEntity>(
            _fileConnectionFactoryMock.Object,
            _entityMock.Object
        );
    }

    [Fact]
    public async Task ExecuteAsync_UpdatesEntity_WhenEntityExists()
    {
        // Define Input and Output
        _ = _fileConnectionFactoryMock
            .Setup(factory => factory.LoadDataAsync())
            .ReturnsAsync(_existingData);

        // Execute actual operation
        Result result = await _updateJsonCommand.ExecuteAsync();

        // Verify actual result
        Assert.True(result.IsSuccess);
        _fileConnectionFactoryMock.Verify(
            factory => factory.OverrideDataAsync(It.Is<IEnumerable<IEntity>>(data =>
                data.Contains(_entityMock.Object))),
            Times.Once
        );
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsError_WhenEntityDoesNotExist()
    {
        // Define Input and Output
        Mock<IEntity> nonExistentEntityMock = new();
        _ = nonExistentEntityMock.SetupGet(e => e.Id).Returns(Guid.NewGuid()); // Different ID

        _ = _fileConnectionFactoryMock
            .Setup(factory => factory.LoadDataAsync())
            .ReturnsAsync(_existingData); // Existing data does not contain the new entity

        // Execute actual operation
        Result result = await new UpdateJsonCommand<IEntity>(
            _fileConnectionFactoryMock.Object,
            nonExistentEntityMock.Object
        ).ExecuteAsync();

        // Verify actual result
        Assert.False(result.IsSuccess);
        _fileConnectionFactoryMock.Verify(factory => factory.SaveDataAsync(It.IsAny<IEnumerable<IEntity>>()), Times.Never);
    }
}
