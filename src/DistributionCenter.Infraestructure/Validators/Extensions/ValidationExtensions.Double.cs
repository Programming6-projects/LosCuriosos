namespace DistributionCenter.Infraestructure.Validators.Extensions;

using Components.Builders.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

public static partial class ValidationExtensions
{
    public static IValidationBuilder<double?> WhenNotNull(this IValidationBuilder<double?> builder)
    {
        return builder.When(static x => x != null);
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

}
