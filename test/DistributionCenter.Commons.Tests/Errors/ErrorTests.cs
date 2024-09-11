namespace DistributionCenter.Commons.Tests.Errors;

using DistributionCenter.Commons.Enums;
using DistributionCenter.Commons.Errors;

public class ErrorTests
{
    [Fact]
    public void Conflict_ReturnsExpectedError()
    {
        // Define Input and output
        string expectedCode = "General.Conflict";
        string expectedDescription = "A 'Conflict' error has occurred";
        ErrorType expectedType = ErrorType.Conflict;
        Error error = Error.Conflict();

        // Execute actual operation
        string actualCode = error.Code;
        string actualDescription = error.Description;
        ErrorType actualType = error.Type;

        // Verify actual result
        Assert.Equal(expectedCode, actualCode);
        Assert.Equal(expectedDescription, actualDescription);
        Assert.Equal(expectedType, actualType);
    }

    [Fact]
    public void Validation_ReturnsExpectedError()
    {
        // Define Input and output
        string expectedCode = "General.Validation";
        string expectedDescription = "A 'Validation' error has occurred";
        ErrorType expectedType = ErrorType.Validation;
        Error error = Error.Validation();

        // Execute actual operation
        string actualCode = error.Code;
        string actualDescription = error.Description;
        ErrorType actualType = error.Type;

        // Verify actual result
        Assert.Equal(expectedCode, actualCode);
        Assert.Equal(expectedDescription, actualDescription);
        Assert.Equal(expectedType, actualType);
    }

    [Fact]
    public void NotFound_ReturnsExpectedError()
    {
        // Define Input and output
        string expectedCode = "General.NotFound";
        string expectedDescription = "A 'Not Found' error has occurred";
        ErrorType expectedType = ErrorType.NotFound;
        Error error = Error.NotFound();

        // Execute actual operation
        string actualCode = error.Code;
        string actualDescription = error.Description;
        ErrorType actualType = error.Type;

        // Verify actual result
        Assert.Equal(expectedCode, actualCode);
        Assert.Equal(expectedDescription, actualDescription);
        Assert.Equal(expectedType, actualType);
    }

    [Fact]
    public void Unauthorized_ReturnsExpectedError()
    {
        // Define Input and output
        string expectedCode = "General.Unauthorized";
        string expectedDescription = "An 'Unauthorized' error has occurred";
        ErrorType expectedType = ErrorType.Unauthorized;
        Error error = Error.Unauthorized();

        // Execute actual operation
        string actualCode = error.Code;
        string actualDescription = error.Description;
        ErrorType actualType = error.Type;

        // Verify actual result
        Assert.Equal(expectedCode, actualCode);
        Assert.Equal(expectedDescription, actualDescription);
        Assert.Equal(expectedType, actualType);
    }

    [Fact]
    public void Unexpected_ReturnsExpectedError()
    {
        // Define Input and output
        string expectedCode = "General.Unexpected";
        string expectedDescription = "An 'Unexpected' error has occurred";
        ErrorType expectedType = ErrorType.Unexpected;
        Error error = Error.Unexpected();

        // Execute actual operation
        string actualCode = error.Code;
        string actualDescription = error.Description;
        ErrorType actualType = error.Type;

        // Verify actual result
        Assert.Equal(expectedCode, actualCode);
        Assert.Equal(expectedDescription, actualDescription);
        Assert.Equal(expectedType, actualType);
    }
}
