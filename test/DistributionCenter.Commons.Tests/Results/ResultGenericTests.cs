namespace DistributionCenter.Commons.Tests.Results;

using System.Collections.ObjectModel;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results;
using Xunit;

public class ResultGenericTests
{
    [Fact]
    public void ResultGeneric_Success_IsTrue_WhenNoErrors()
    {
        // Define Input and Output
        Result<int> result;

        // Execute actual operation
        result = 1;

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void ResultGeneric_Success_IsFalse_WhenErrorsExist()
    {
        // Define Input and Output
        Result<int> result;
        Error error = Error.Conflict("Test", "Test Error");

        // Execute actual operation
        result = error;

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void ResultGeneric_Errors_ReturnsErrors_WhenErrorsExist()
    {
        // Define Input and Output
        Result<int> result;
        Error error = Error.Conflict("Test", "Test Error");

        // Execute actual operation
        result = error;

        // Verify actual result
        Assert.Contains(error, result.Errors);
    }

    [Fact]
    public void ResultGeneric_Value_ReturnsValue_WhenNoErrors()
    {
        // Define Input and Output
        int expectedResult = 1;
        Result<int> result;

        // Execute actual operation
        result = 1;

        // Verify actual result
        Assert.Equal(expectedResult, result.Value);
    }

    [Fact]
    public void ResultGeneric_Value_ThrowsException_WhenErrorsExist()
    {
        // Define Input and Ouput
        Result<int> result;
        Error error = Error.Conflict("Test", "Test Error");

        // Execute actual operation
        result = error;

        // Verify actual result
        _ = Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    [Fact]
    public void ResultGeneric_Constructor_CreatesInstance_WithErrorsCollection()
    {
        // Define Input and Output
        Result<int> result;
        Collection<IError> errors = [Error.Conflict("Test", "Test Error")];

        // Execute actual operation
        result = errors;

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal(errors, result.Errors);
    }

    [Fact]
    public void ResultGeneric_Constructors_CreatesInstance_WithErrorsArray()
    {
        // Define Input and Output
        Result<int> result;
        Error error = Error.Conflict("Test", "Test Error");
        Error[] errors = [error];

        // Execute actual operation
        result = errors;

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(error, result.Errors);
    }

    [Fact]
    public void ResultGeneric_Execute_SuccesfullOperation_OnMatch()
    {
        // Define Input and Output
        int expectedValue = 2;
        Result<int> result = expectedValue;

        // Execute actual operation
        int actualValue = result.Match(static v => v, static _ => 1);

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void ResultGeneric_Execute_FailedOperation_OnMatch()
    {
        // Define Input and Output
        int expectedValue = 1;
        Result<int> result = Error.Conflict("Test", "Test Error");

        // Execute actual operation
        int actualValue = result.Match(static v => v, static _ => 1);

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public async Task Result_Execute_AsyncSuccesfullOperation_OnMatchAsync()
    {
        // Define Input and Output
        int expectedValue = 2;
        Result<int> result = expectedValue;

        // Execute actual operation
        int actualValue = await result.MatchAsync(static v => Task.FromResult(v), static _ => Task.FromResult(1));

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public async Task Result_Execute_AsyncFailedOperation_OnMatchAsync()
    {
        // Define Input and Output
        int expectedValue = 1;
        Result<int> result = Error.Conflict("Test", "Test Error");

        // Execute actual operation
        int actualValue = await result.MatchAsync(static v => Task.FromResult(v), static _ => Task.FromResult(1));

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }
}
