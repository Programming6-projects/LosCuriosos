namespace DistributionCenter.Infraestructure.Tests.Validators.DeliveryPoint;

using Commons.Results;
using Infraestructure.DTOs.Concretes.DeliveryPoint;
using Infraestructure.Validators.Core.Concretes.DeliveryPoint;

public class UpdateDeliveryPointValidatorTests
{
    [Fact]
    public void Validate_ShouldReturnError_WhenLongitudeIsZero()
    {
        UpdateDeliveryPointDto dto = new () { Latitude = 45.0, Longitude = 0 };
        UpdateDeliveryPointValidator validator = new ();

        Result result = validator.Validate(dto);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenBothLatitudeAndLongitudeAreZero()
    {
        UpdateDeliveryPointDto dto = new () { Latitude = 0, Longitude = 0 };
        UpdateDeliveryPointValidator validator = new ();

        Result result = validator.Validate(dto);

        // Assert
        Assert.False(result.IsSuccess);
    }
}
