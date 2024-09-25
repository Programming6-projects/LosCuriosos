namespace DistributionCenter.Infraestructure.Tests.Validators.Strikes;

using DistributionCenter.Infraestructure.DTOs.Concretes.Strikes;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Strikes;
using Xunit;

public class UpdateStrikeValidatorTest
{
    [Fact]
    public void VerifyThanStrikeDescriptionIsValid()
    {
        // Define Input and Output
        UpdateStrikeValidator validator = new();
        string invalidDescription = "";
        string validDescription = "Valid description";

        // Execute actual operation
        UpdateStrikeDto invalidDto = new() { Description = invalidDescription, TransportId = Guid.NewGuid() };
        UpdateStrikeDto validDto = new() { Description = validDescription, TransportId = Guid.NewGuid() };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionHasAtLeast3Characters()
    {
        // Define Input and Output
        UpdateStrikeValidator validator = new();
        string invalidDescription = "a";
        string validDescription = "Valid description";

        // Execute actual operation
        UpdateStrikeDto invalidDto = new() { Description = invalidDescription, TransportId = Guid.NewGuid() };
        UpdateStrikeDto validDto = new() { Description = validDescription, TransportId = Guid.NewGuid() };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionHasLeastThan128Characters()
    {
        // Define Input and Output
        UpdateStrikeValidator validator = new();
        string invalidDescription = new('a', 129);
        string validDescription = new('a', 128);

        // Execute actual operation
        UpdateStrikeDto invalidDto = new() { Description = invalidDescription, TransportId = Guid.NewGuid() };
        UpdateStrikeDto validDto = new() { Description = validDescription, TransportId = Guid.NewGuid() };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}
