namespace DistributionCenter.Application.Tests.Tables.Components.QueryCommands.Bases;

using System.Collections.ObjectModel;
using Application.Tables.Components.QueryCommands.Bases;
using Commons.Errors;
using Commons.Errors.Interfaces;
using Commons.Results;
using Domain.Entities.Interfaces;

public class BaseMultipleResponseQueryTests
{
    private readonly Mock<BaseMultipleResponseQuery<IEntity>> _baseQuery;

    public BaseMultipleResponseQueryTests()
    {
        // Mock the BaseQuery with CallBase = true
        _baseQuery = new Mock<BaseMultipleResponseQuery<IEntity>>() { CallBase = true };
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSuccessResult()
    {
        // Define Input and Output
        Result<IEnumerable<IEntity>> expectedResult = new(new Mock<IEnumerable<IEntity>>().Object);

        // Mock the protected ExecuteAsync method
        _ = _baseQuery.Setup(q => q.ExecuteAsync()).ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEnumerable<IEntity>> result = await _baseQuery.Object.ExecuteAsync();

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
        Result<IEnumerable<IEntity>> expectedResult = errors;

        // Mock the protected ExecuteAsync method to return an error result
        _ = _baseQuery.Setup(q => q.ExecuteAsync()).ReturnsAsync(expectedResult);

        // Execute actual operation
        Result<IEnumerable<IEntity>> result = await _baseQuery.Object.ExecuteAsync();

        // Verify actual result
        _baseQuery.Verify(q => q.ExecuteAsync(), Times.Once);
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedResult.Errors, result.Errors);
    }
}
