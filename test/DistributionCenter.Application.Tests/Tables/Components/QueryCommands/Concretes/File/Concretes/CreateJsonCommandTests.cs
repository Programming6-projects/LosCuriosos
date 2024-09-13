namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.File.Concretes;

using Application.Tables.Components.QueryCommands.Concretes.File.Concretes;
using Application.Tables.Connections.File.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;

public class CreateJsonCommandTests
{
    private readonly CreateJsonCommand<IEntity> _createJsonCommand;
    private readonly Mock<IFileConnectionFactory<IEntity>> _fileConnectionFactoryMock;
    private readonly Mock<IEntity> _entityMock;
    private readonly List<IEntity> _existingData;

    public CreateJsonCommandTests()
    {
        _fileConnectionFactoryMock = new Mock<IFileConnectionFactory<IEntity>>();
        _entityMock = new Mock<IEntity>();
        _existingData = [_entityMock.Object];

        _createJsonCommand = new CreateJsonCommand<IEntity>(
            _fileConnectionFactoryMock.Object,
            _entityMock.Object
        );
    }

    [Fact]
    public async Task ExecuteAsync_CallsSaveDataAsyncWithCorrectData()
    {
        // Define Input and Output
         _ = _fileConnectionFactoryMock
            .Setup(factory => factory.LoadDataAsync())
            .ReturnsAsync(_existingData);
        _ = _fileConnectionFactoryMock
            .Setup(factory => factory.SaveDataAsync(It.IsAny<IEnumerable<IEntity>>()))
            .Returns(Task.CompletedTask);

        // Execute actual operation
        Result result = await _createJsonCommand.ExecuteAsync();

        // Verify actual result
        _fileConnectionFactoryMock.Verify(factory => factory.SaveDataAsync(It.Is<IEnumerable<IEntity>>(data => data.Contains(_entityMock.Object))), Times.Once);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSuccessResult()
    {
        // Define Input and Output
        _ = _fileConnectionFactoryMock
            .Setup(factory => factory.LoadDataAsync())
            .ReturnsAsync(_existingData);
        _ = _fileConnectionFactoryMock
            .Setup(factory => factory.SaveDataAsync(It.IsAny<IEnumerable<IEntity>>()))
            .Returns(Task.CompletedTask);

        // Execute actual operation
        Result result = await _createJsonCommand.ExecuteAsync();

        // Verify actual result
        Assert.True(result.IsSuccess);
    }
}
