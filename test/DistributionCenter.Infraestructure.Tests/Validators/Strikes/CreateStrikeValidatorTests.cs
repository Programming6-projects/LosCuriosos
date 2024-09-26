namespace DistributionCenter.Infraestructure.Tests.Validators.Strikes;

using DistributionCenter.Infraestructure.DTOs.Concretes.Strikes;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Strikes;
using Xunit;

public class CreateStrikeValidatorTest
{
    [Fact]
    public void VerifyThanStrikeDescriptionHasAtLeast3Characters()
    {
        CreateStrikeValidator validator = new();
        string invalidDescription = "a";
        string validDescription = "Valid description";

        CreateStrikeDto invalidDto = new() { Description = invalidDescription, TransportId = Guid.NewGuid() };
        CreateStrikeDto validDto = new() { Description = validDescription, TransportId = Guid.NewGuid() };

        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionHasLeastThan128Characters()
    {
        CreateStrikeValidator validator = new();
        string invalidDescription = new('a', 129);
        string validDescription = new('a', 128);

        CreateStrikeDto invalidDto = new() { Description = invalidDescription, TransportId = Guid.NewGuid() };
        CreateStrikeDto validDto = new() { Description = validDescription, TransportId = Guid.NewGuid() };

        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}
