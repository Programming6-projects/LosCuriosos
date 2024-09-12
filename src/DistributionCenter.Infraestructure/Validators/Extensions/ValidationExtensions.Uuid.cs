namespace DistributionCenter.Infraestructure.Validators.Extensions;

using Components.Builders.Interfaces;

public static partial class ValidationExtensionsUuid
{
    public static IValidationBuilder<Guid?> UuidNotNull(this IValidationBuilder<Guid?> builder, string message)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder.When(static x => x.HasValue).AddRule(static x => x != Guid.Empty, message)!;
    }
}
