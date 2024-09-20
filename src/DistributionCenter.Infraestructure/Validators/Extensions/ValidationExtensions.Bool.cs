namespace DistributionCenter.Infraestructure.Validators.Extensions;

using Components.Builders.Interfaces;

public static partial class ValidationExtensions
{
    public static IValidationBuilder<bool?> WhenNotNull(this IValidationBuilder<bool?> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.When(static x => x != null);
    }
}
