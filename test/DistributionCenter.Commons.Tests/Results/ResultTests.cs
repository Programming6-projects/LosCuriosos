namespace DistributionCenter.Commons.Tests.Results;

using System.Collections.ObjectModel;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Errors.Interfaces;
using DistributionCenter.Commons.Results;
using Xunit;

public class ResultTests
{
    [Fact]
    public void Result_Success_IsTrue_WhenNoErrors()
    {
        // Define Input and Output
        Result result;

        // Execute actual operation
        result = Result.Ok();

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Result_Success_IsFalse_WhenErrorsExist()
    {
        // Define Input and Output
        Result result;
        Error error = Error.Conflict("Test", "Test Error");

        // Execute actual operation
        result = error;

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Result_Errors_ReturnsErrors_WhenErrorsExist()
    {
        // Define Input and Output
        Result result;
        Error error = Error.Conflict("Test", "Test Error");

        // Execute actual operation
        result = error;

        // Verify actual result
        Assert.Contains(error, result.Errors);
    }

    [Fact]
    public void Result_Errors_ThrowsException_WhenNoErrors()
    {
        // Define Input and Output
        Result result;

        // Execute actual operation
        result = Result.Ok();

        // Verify actual result
        _ = Assert.Throws<InvalidOperationException>(() => result.Errors);
    }

    [Fact]
    public void Result_Constructor_CreatesInstance_WithErrorsCollection()
    {
        // Define Input and Output
        Result result;
        Collection<IError> errors = [Error.Conflict("Test", "Test Error")];

        // Execute actual operation
        result = errors;

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal(errors, result.Errors);
    }

    [Fact]
    public void Result_Constructors_CreatesInstance_WithErrorsArray()
    {
        // Define Input and Output
        Result result;
        Error error = Error.Conflict("Test", "Test Error");
        Error[] errors = [error];

        // Execute actual operation
        result = errors;

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Contains(error, result.Errors);
    }

    [Fact]
    public void Result_Execute_SuccesfullOperation_OnMatch()
    {
        // Define Input and Output
        Result result = Result.Ok();
        int expectedValue = 2;

        // Execute actual operation
        int actualValue = result.Match(static () => 2, static _ => 1);

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void Result_Execute_FailedOperation_OnMatch()
    {
        // Define Input and Output
        Result result = Error.Conflict("Test", "Test Error");
        int expectedValue = 1;

        // Execute actual operation
        int actualValue = result.Match(static () => 2, static _ => 1);

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public async Task Result_Execute_AsyncSuccesfullOperation_OnMatchAsync()
    {
        // Define Input and Output
        Result result = Result.Ok();
        int expectedValue = 2;

        // Execute actual operation
        int actualValue = await result.MatchAsync(static () => Task.FromResult(2), static _ => Task.FromResult(1));

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public async Task Result_Execute_AsyncFailedOperation_OnMatchAsync()
    {
        // Define Input and Output
        Result result = Error.Conflict("Test", "Test Error");
        int expectedValue = 1;

        // Execute actual operation
        int actualValue = await result.MatchAsync(static () => Task.FromResult(2), static _ => Task.FromResult(1));

        // Verify actual result
        Assert.Equal(expectedValue, actualValue);
    }
}
