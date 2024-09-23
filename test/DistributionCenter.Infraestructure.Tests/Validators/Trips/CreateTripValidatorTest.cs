namespace DistributionCenter.Infraestructure.Tests.Validators.Trips;

using Infraestructure.DTOs.Concretes.Trip;
using Infraestructure.Validators.Core.Concretes.Trip;

public class CreateTripValidatorTest
{
    [Fact]
    public void ValidateThatStatusCorrespondToEnum()
    {
        // Define Input and Output
        CreateTripValidator validator = new();
        string invalidStatus = "NoPending";
        string validStatus = "Pending";

        // Execute actual operation
        CreateTripDto invalidDto = new()
        {
            Status = invalidStatus
        };
        CreateTripDto validDto = new()
        {
            Status = validStatus
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}
