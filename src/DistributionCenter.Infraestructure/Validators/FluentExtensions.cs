namespace DistributionCenter.Infraestructure.Validators;

using FluentValidation;

public static class FluentExtensions
{
    public static IRuleBuilderOptions<TIn, TOut> NotNullNotEmpty<TIn, TOut>(
        this IRuleBuilderInitial<TIn, TOut> ruleBuilder,
        string propertyName
    )
    {
        return ruleBuilder
            .NotNull()
            .WithMessage($"{propertyName} is required.")
            .NotEmpty()
            .WithMessage($"{propertyName} cannot be empty.");
    }

    public static IRuleBuilderOptions<TIn, string?> SizeRange<TIn>(
        this IRuleBuilderOptions<TIn, string?> ruleBuilder,
        string propertyName,
        int min,
        int max
    )
    {
        return ruleBuilder
            .MinimumLength(min)
            .WithMessage($"{propertyName} must be at least {min} characters.")
            .MaximumLength(max)
            .WithMessage($"{propertyName} must be at most {max} characters.");
    }
}
