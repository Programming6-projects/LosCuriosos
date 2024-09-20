namespace DistributionCenter.Infraestructure.Tests.Validators;

using Infraestructure.Validators.Components.Builders.Concretes;
using Infraestructure.Validators.Extensions;

public class ValidationExtensionsTests
{
    [Fact]
    public void CheckedThanTheLimitOfTheDecimalNumbersWasValidated()
    {
        double validNumber = 1345.52;
        double invalidNumber = 1345.5242;
        ValidationBuilder<double> validations = new();
        _ = validations.DecimalSize(2, "The number couldn't has more than 2 decimal numbers");

        Assert.Empty(validations.Validate(validNumber));
        Assert.NotEmpty(validations.Validate(invalidNumber));
    }

    [Fact]
    public void NumberRange_ShouldValidateThatValueIsWithinRange()
    {
        // Arrange
        ValidationBuilder<int?> validations = new();
        _ = validations.NumberRange(5, 10, "Value must be between 5 and 10.");

        // Act
        int? validValue = 7;
        int? invalidValue = 11;

        // Assert
        Assert.Empty(validations.Validate(validValue)); // Should pass
        Assert.NotEmpty(validations.Validate(invalidValue)); // Should fail
    }

    [Fact]
    public void NonNegatives_ShouldValidateThatValueIsNonNegative()
    {
        // Arrange
        ValidationBuilder<int?> validations = new();
        _ = validations.NonNegatives("Value cannot be negative.");

        // Act
        int? validValue = 0; // Edge case
        int? validPositiveValue = 10;
        int? invalidValue = -1;

        // Assert
        Assert.Empty(validations.Validate(validValue)); // Should pass
        Assert.Empty(validations.Validate(validPositiveValue)); // Should pass
        Assert.NotEmpty(validations.Validate(invalidValue)); // Should fail
    }
}
