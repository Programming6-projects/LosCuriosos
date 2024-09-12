namespace DistributionCenter.Infraestructure.Validators.Extensions;

using System.Globalization;
using System.Text.RegularExpressions;
using DistributionCenter.Infraestructure.Validators.Components.Builders.Interfaces;

public static class ValidationExtensions
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

    public static IValidationBuilder<double> DecimalSize(
        this IValidationBuilder<double> builder,
        int decimalQuantity,
        string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(x =>
        {
            string number = x.ToString(CultureInfo.InvariantCulture);

            return Regex.IsMatch(number, $@"^\d+(\.\d{{{decimalQuantity}}})?$");

        }, message);
    }

    public static IValidationBuilder<int> NumberRange(
        this IValidationBuilder<int> builder,
        uint? min,
        uint? max,
        string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(x => (x >= (min ?? uint.MinValue) && x <= (max ?? uint.MaxValue)), message);
    }

    public static IValidationBuilder<int> NonNegatives(this IValidationBuilder<int> builder, string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(x => x >= 0, message);
    }
}
