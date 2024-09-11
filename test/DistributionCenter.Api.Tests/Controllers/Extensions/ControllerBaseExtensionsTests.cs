namespace DistributionCenter.Api.Tests.Controllers.Extensions;

using DistributionCenter.Api.Controllers.Extensions;
using DistributionCenter.Commons.Enums;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Errors.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class ControllerBaseExtensionsTests
{
    private readonly Mock<ControllerBase> _controllerMock;

    public ControllerBaseExtensionsTests()
    {
        _controllerMock = new Mock<ControllerBase>() { CallBase = true };
    }

    [Fact]
    public void ErrorsResponse_MapValidationErrors_ReturnActionResult()
    {
        // Define Input and Output
        ICollection<IError> errors = [Error.Validation(), Error.Validation()];

        // Execute actual operation
        IActionResult result = _controllerMock.Object.ErrorsResponse(errors);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        ValidationProblemDetails problemDetails = Assert.IsType<ValidationProblemDetails>(objectResult.Value);
        IDictionary<string, string[]> modelState = problemDetails.Errors;

        _ = Assert.Single(modelState);
    }

    [Fact]
    public void ErrorsResponse_ConflictError_ReturnsObjectResult()
    {
        // Define Input and Output
        ICollection<IError> errors = [Error.Conflict()];

        // Execute actual operation
        IActionResult result = _controllerMock.Object.ErrorsResponse(errors);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        ProblemDetails problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal(409, problemDetails.Status);
    }

    [Fact]
    public void ErrorsResponse_UnexpectedError_ReturnsObjectResult()
    {
        // Define Input and Output
        ICollection<IError> errors = [Error.Unexpected()];

        // Execute actual operation
        IActionResult result = _controllerMock.Object.ErrorsResponse(errors);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        ProblemDetails problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal(400, problemDetails.Status);
    }

    [Fact]
    public void ErrorsResponse_ValidationError_ReturnsObjectResult()
    {
        // Define Input and Output
        ICollection<IError> errors = [Error.Validation(), Error.Conflict()];

        // Execute actual operation
        IActionResult result = _controllerMock.Object.ErrorsResponse(errors);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        ProblemDetails problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal(400, problemDetails.Status);
    }

    [Fact]
    public void ErrorsResponse_NotFoundError_ReturnsObjectResult()
    {
        // Define Input and Output
        ICollection<IError> errors = [Error.NotFound()];

        // Execute actual operation
        IActionResult result = _controllerMock.Object.ErrorsResponse(errors);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        ProblemDetails problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal(404, problemDetails.Status);
    }

    [Fact]
    public void ErrorsResponse_UnauthorizedError_ReturnsObjectResult()
    {
        // Define Input and Output
        ICollection<IError> errors = [Error.Unauthorized()];

        // Execute actual operation
        IActionResult result = _controllerMock.Object.ErrorsResponse(errors);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        ProblemDetails problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal(403, problemDetails.Status);
    }

    [Fact]
    public void ErrorsResponse_UnknownErrorType_ReturnsInternalServerErrorResult()
    {
        // Define Input and Output
        Error error = Error.Conflict();
        error.Type = (ErrorType)999;
        ICollection<IError> errors = [error];

        // Execute actual operation
        IActionResult result = _controllerMock.Object.ErrorsResponse(errors);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        ProblemDetails problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal(500, problemDetails.Status);
    }
}
