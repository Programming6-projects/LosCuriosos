namespace DistributionCenter.Infraestructure.Behaviors;

using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    private readonly IValidator<TRequest>? _validator = validator;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(next, nameof(next));

        if (_validator is null)
        {
            return await next();
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        List<Error> errors = validationResult.Errors.ConvertAll(static error =>
            Error.Validation(code: error.ErrorCode, description: error.ErrorMessage)
        );

        return (dynamic)errors.ToArray();
    }
}
