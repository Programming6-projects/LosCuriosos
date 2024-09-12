namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.File.Bases;

using Application.Tables.Components.QueryCommands.Concretes.File.Bases;
using Application.Tables.Connections.File.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;
using Moq.Protected;

public class BaseJsonCommandTests
{
    private readonly Mock<IFileConnectionFactory<IEntity>> _fileConnectionFactory;
    private readonly Mock<BaseJsonCommand<IEntity>> _baseJsonCommand;

    public BaseJsonCommandTests()
    {
        _fileConnectionFactory = new Mock<IFileConnectionFactory<IEntity>>();
        _baseJsonCommand = new Mock<BaseJsonCommand<IEntity>>(
            _fileConnectionFactory.Object,
            new Mock<IEntity>().Object
        )
        {
            CallBase = true,
        };
    }

    [Fact]
    public async Task ExecuteAsync_CallsLoadDataAsyncAndExecute()
    {
        // Define Input and Output
        List<IEntity> mockData = [new Mock<IEntity>().Object];
        _ = _fileConnectionFactory.Setup(static m => m.LoadDataAsync()).ReturnsAsync(mockData);
        _ = _baseJsonCommand
            .Protected()
            .Setup<Task<Result>>("Execute", ItExpr.IsAny<IEnumerable<IEntity>>())
            .ReturnsAsync(Result.Ok());

        // Execute actual operation
        Result result = await _baseJsonCommand.Object.ExecuteAsync();

        // Verify actual result
        _fileConnectionFactory.Verify(static m => m.LoadDataAsync(), Times.Once);
        _baseJsonCommand.Protected().Verify("Execute", Times.Once(), ItExpr.IsAny<IEnumerable<IEntity>>());

        Assert.True(result.IsSuccess);
    }
}
