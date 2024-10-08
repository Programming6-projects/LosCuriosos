namespace DistributionCenter.Infraestructure.Validators.Extensions;

using System.Text.RegularExpressions;
using DistributionCenter.Infraestructure.Validators.Components.Builders.Interfaces;
using Domain.Entities.Enums;

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

    public static IValidationBuilder<string> BelongsToStatus(this IValidationBuilder<string> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder
            .AddRule(static x => Enum.TryParse(x, true, out Status _)
                , "String don't correspond to Status Enum");
    }
}
