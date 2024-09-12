namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Concretes.File.Bases;

using System.Collections.ObjectModel;
using Application.Tables.Components.QueryCommands.Concretes.File.Bases;
using Application.Tables.Connections.Interfaces;
using Commons.Errors;
using Commons.Errors.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;
using Moq.Protected;

public class BaseJsonQueryTests
{
    private readonly Mock<BaseJsonQuery<IEntity>> _jsonQuery;
    private readonly Mock<IFileConnectionFactory<IEntity>> _fileConnectionFactory;

    public BaseJsonQueryTests()
    {
        _fileConnectionFactory = new Mock<IFileConnectionFactory<IEntity>>();
        _jsonQuery = new Mock<BaseJsonQuery<IEntity>>(_fileConnectionFactory.Object) { CallBase = true };
    }

    [Fact]
    public async Task ExecuteAsync_CallsLoadDataAsyncAndExecute()
    {
        // Define Input and Output
        List<IEntity> mockData = new() { new Mock<IEntity>().Object };
        Result<IEntity> expectedResult = new(new Mock<IEntity>().Object);

        _ = _fileConnectionFactory.Setup(static m => m.LoadDataAsync()).ReturnsAsync(mockData);
        _ = _jsonQuery
            .Protected()
            .Setup<Task<Result<IEntity>>>("Execute", ItExpr.IsAny<IEnumerable<IEntity>>())
            .ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEntity> result = await _jsonQuery.Object.ExecuteAsync();

        // Verify actual result
        _fileConnectionFactory.Verify(static m => m.LoadDataAsync(), Times.Once);
        _jsonQuery.Protected().Verify("Execute", Times.Once(), ItExpr.IsAny<IEnumerable<IEntity>>());

        Assert.True(result.IsSuccess);
        Assert.Equal(expectedResult.Value, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsErrorResult()
    {
        // Define Input and Output
        Collection<IError> errors = [Error.Conflict()];
        Result<IEntity> expectedResult = errors;
        List<IEntity> mockData = [];

        _ = _fileConnectionFactory.Setup(static m => m.LoadDataAsync()).ReturnsAsync(mockData);
        _ = _jsonQuery
            .Protected()
            .Setup<Task<Result<IEntity>>>("Execute", ItExpr.IsAny<IEnumerable<IEntity>>())
            .ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEntity> result = await _jsonQuery.Object.ExecuteAsync();

        // Verify actual result
        _fileConnectionFactory.Verify(static m => m.LoadDataAsync(), Times.Once);
        _jsonQuery.Protected().Verify("Execute", Times.Once(), ItExpr.IsAny<IEnumerable<IEntity>>());

        Assert.False(result.IsSuccess);
        Assert.Equal(expectedResult.Errors, result.Errors);
    }
}
