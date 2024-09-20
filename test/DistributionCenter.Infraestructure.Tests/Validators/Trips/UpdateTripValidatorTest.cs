namespace DistributionCenter.Infraestructure.Tests.Validators.Trips;

using Infraestructure.DTOs.Concretes.Trip;
using Infraestructure.Validators.Core.Concretes.Trip;

public class UpdateTripValidatorTest
{
    [Fact]
    public void ValidateThatStatusCorrespondToEnum()
    {
        // Define Input and Output
        UpdateTripValidator validator = new();
        string invalidStatus = "NoPending";
        string validStatus = "Pending";

        // Execute actual operation
        UpdateTripDto invalidDto = new()
        {
            Status = invalidStatus
        };
        UpdateTripDto validDto = new()
        {
            Status = validStatus
        };

        // Verify actual result
        Assert.False(validator.Validate(invalidDto).IsSuccess);
        Assert.True(validator.Validate(validDto).IsSuccess);
    }
}
