namespace DistributionCenter.Infraestructure.Validators.Extensions;

using Components.Builders.Interfaces;

public static partial class ValidationExtensionsUuid
{
    public static IValidationBuilder<Guid> NotNullNotEmtpy(this IValidationBuilder<Guid> builder, string message)
    {
        return builder.AddRule(static x => x != Guid.Empty, message);
    }

    public static IValidationBuilder<Guid> WhenNotNull(this IValidationBuilder<Guid?> builder)
    {
        return (IValidationBuilder<Guid>)builder.When(static x => x != null)!;
    }
}
