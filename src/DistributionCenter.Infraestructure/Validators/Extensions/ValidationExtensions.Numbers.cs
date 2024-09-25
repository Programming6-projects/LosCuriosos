namespace DistributionCenter.Infraestructure.Validators.Extensions;

using System.Globalization;
using System.Text.RegularExpressions;
using Components.Builders.Interfaces;

public static partial  class ValidationExtensions
{
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

    public static IValidationBuilder<int?> WhenNotNull(this IValidationBuilder<int?> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.When(static x => x != null);
    }

    public static IValidationBuilder<double?> WhenNotNull(this IValidationBuilder<double?> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        return builder.When(static x => x != null);
    }

    public static IValidationBuilder<double> WhenNotEmpty(this IValidationBuilder<double> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        return builder.When(static x => x != 0);
    }

    public static IValidationBuilder<double?> WhenNotEmpty(this IValidationBuilder<double?> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        return builder.When(static x => x != 0);
    }

    public static IValidationBuilder<int?> NumberRange(
        this IValidationBuilder<int?> builder,
        uint? min,
        uint? max,
        string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(x => x.HasValue &&
                                    x.Value >= (min ?? uint.MinValue) &&
                                    x.Value <= (max ?? uint.MaxValue),
            message);
    }

    public static IValidationBuilder<int?> NonNegatives(this IValidationBuilder<int?> builder, string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.AddRule(x => !x.HasValue || x.Value >= 0, message);
    }
}
