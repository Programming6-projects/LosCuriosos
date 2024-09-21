namespace DistributionCenter.Infraestructure.Validators.Extensions;

using System.Text.RegularExpressions;
using Components.Builders.Concretes;
using DistributionCenter.Domain.Entities.Enums;
using DistributionCenter.Infraestructure.Validators.Components.Builders.Interfaces;

public static partial class ValidationExtensions
{
    public static IValidationBuilder<string> WhenNotNull(this IValidationBuilder<string?> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.When(static x => x != null)!;
    }

    public static IValidationBuilder<string> NotNullNotEmpty(this IValidationBuilder<string> builder, string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(static x => !string.IsNullOrEmpty(x), message);
    }

    public static IValidationBuilder<string> SizeRange(
        this IValidationBuilder<string> builder,
        int min,
        int max,
        string message
    )
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(x => x.Length >= min && x.Length <= max, message);
    }

    public static IValidationBuilder<string> RegexValidator(
        this IValidationBuilder<string> builder,
        string pattern,
        string message
    )
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(x => Regex.IsMatch(x, pattern), message);
    }

    public static IValidationBuilder<string> EmailValidator(this IValidationBuilder<string> builder, string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(static x => Regex.IsMatch(x, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"), message);
    }

    public static IValidationBuilder<Status> BelongsToStatus(this IValidationBuilder<string> builder)
    {
        ValidationBuilder<Status> enumBuilder = new();

        _ = enumBuilder.When(value => builder.Validate(value.ToString()).Count == 0)
            .AddRule(
                value => Enum.TryParse(value.ToString(), true, out Status _),
                "The value isn't a valid Status."
            );

        return enumBuilder;
    }
}
