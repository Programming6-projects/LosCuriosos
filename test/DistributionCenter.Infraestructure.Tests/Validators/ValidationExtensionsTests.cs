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
}
