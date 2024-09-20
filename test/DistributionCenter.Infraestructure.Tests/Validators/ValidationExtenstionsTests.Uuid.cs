namespace DistributionCenter.Infraestructure.Tests.Validators;

using DistributionCenter.Infraestructure.Validators.Components.Builders.Interfaces;
using DistributionCenter.Infraestructure.Validators.Extensions;

public class ValidationExtensionsUuidTests
{
    [Fact]
    public void UuidNotNull_ThrowsArgumentNullException_WhenBuilderIsNull()
    {
        // Arrange
        IValidationBuilder<Guid>? builder = null;
        string message = "UUID cannot be empty";

        // Act & Assert
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => ValidationExtensionsUuid.UuidNotNull(builder!, message)
        );
        Assert.Equal("builder", exception.ParamName);
    }

    [Fact]
    public void UuidNotNull_AddsRuleWithCorrectLogic()
    {
        // Arrange
        Mock<IValidationBuilder<Guid>> mockBuilder = new();
        string message = "UUID cannot be empty";
        Func<Guid, bool>? rule = null;

        _ = mockBuilder
            .Setup(builder => builder.AddRule(It.IsAny<Func<Guid, bool>>(), message))
            .Callback<Func<Guid, bool>, string>((r, m) => rule = r);

        // Act
        _ = ValidationExtensionsUuid.UuidNotNull(mockBuilder.Object, message);

        // Assert
        Assert.NotNull(rule);
        Assert.True(rule(Guid.NewGuid())); // Valid UUID
        Assert.False(rule(Guid.Empty)); // Invalid UUID
    }
}
