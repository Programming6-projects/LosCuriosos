namespace DistributionCenter.Api.Controllers.Extensions;

using DistributionCenter.Commons.Enums;
using DistributionCenter.Commons.Errors.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public static class ControllerBaseExtensions
{
    public static IActionResult ErrorsResponse(this ControllerBase controller, ICollection<IError> errors)
    {
        ArgumentNullException.ThrowIfNull(controller, nameof(controller));

        if (errors.All(static error => error.Type == ErrorType.Validation))
        {
            return MapValidationErrors(errors, controller);
        }

        return ErrorsResponse(errors.First(), controller);
    }

    private static ObjectResult ErrorsResponse(IError error, ControllerBase controller)
    {
        int statusCode = error.Type switch
        {
            ErrorType.Unexpected => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        return controller.Problem(statusCode: statusCode, title: error.Description);
    }

    private static ActionResult MapValidationErrors(ICollection<IError> errors, ControllerBase controller)
    {
        ModelStateDictionary modelStateDictionary = new();

        List<IError> validationErrors = [.. errors];

        validationErrors.ForEach(error => modelStateDictionary.AddModelError(error.Code, error.Description));

        return controller.ValidationProblem(modelStateDictionary);
    }
}
