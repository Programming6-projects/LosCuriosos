namespace DistributionCenter.Api.Controllers.Bases;

using DistributionCenter.Commons.Enums;
using DistributionCenter.Commons.Errors.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected IActionResult Problem(ICollection<IError> errors)
    {
        if (errors.All(static error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors.First());
    }

    private ObjectResult Problem(IError error)
    {
        int statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unexpected => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private ActionResult ValidationProblem(ICollection<IError> errors)
    {
        ModelStateDictionary modelStateDictionary = new();

        List<IError> validationErrors = [.. errors];

        validationErrors.ForEach(error => modelStateDictionary.AddModelError(error.Code, error.Description));

        return ValidationProblem(modelStateDictionary);
    }
}
