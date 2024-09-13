namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Bases;

using System.Collections.ObjectModel;
using Application.Tables.Components.QueryCommands.Bases;
using Commons.Errors;
using Commons.Errors.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;

public class BaseQueryTests
{
    private readonly Mock<BaseQuery<IEntity>> _baseQuery;

    public BaseQueryTests()
    {
        // Mock the BaseQuery with CallBase = true
        _baseQuery = new Mock<BaseQuery<IEntity>>() { CallBase = true };
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSuccessResult()
    {
        // Define Input and Output
        Result<IEntity> expectedResult = new(new Mock<IEntity>().Object);

        // Mock the protected ExecuteAsync method
        _ = _baseQuery.Setup(q => q.ExecuteAsync()).ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEntity> result = await _baseQuery.Object.ExecuteAsync();

        // Verify actual result
        _baseQuery.Verify(q => q.ExecuteAsync(), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedResult.Value, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsErrorResult()
    {
        // Define Input and Output
        Collection<IError> errors = [Error.Conflict()];
        Result<IEntity> expectedResult = errors;

        // Mock the protected ExecuteAsync method to return an error result
        _ = _baseQuery.Setup(q => q.ExecuteAsync()).ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEntity> result = await _baseQuery.Object.ExecuteAsync();

        // Verify actual result
        _baseQuery.Verify(q => q.ExecuteAsync(), Times.Once);
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedResult.Errors, result.Errors);
    }
}
