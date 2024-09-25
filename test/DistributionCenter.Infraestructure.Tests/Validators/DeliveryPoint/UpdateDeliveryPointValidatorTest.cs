namespace DistributionCenter.Infraestructure.Tests.Validators.DeliveryPoint;

using Commons.Results;
using Infraestructure.DTOs.Concretes.DeliveryPoint;
using Infraestructure.Validators.Core.Concretes.DeliveryPoint;

public class UpdateDeliveryPointValidatorTest
{
    [Fact]
    public void Constructor_ShouldInitializeWithoutExceptions()
    {
        UpdateDeliveryPointValidator validator = new();

        // Assert
        Assert.NotNull(validator);
    }

    [Fact]
    public void Validate_ShouldPass_WhenLatitudeIsNotZero()
    {
        UpdateDeliveryPointDto dto = new()
        {
            Latitude = 1.23,
            Longitude = 4.56
        };
        UpdateDeliveryPointValidator validator = new();

        Result result = validator.Validate(dto);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Validate_ShouldPass_WhenLongitudeIsNotZero()
    {
        UpdateDeliveryPointDto dto = new()
        {
            Latitude = 1.23,
            Longitude = 4.56
        };
        UpdateDeliveryPointValidator validator = new();

        Result result = validator.Validate(dto);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Validate_ShouldPass_WhenLatitudeAndLongitudeAreValid()
    {
        UpdateDeliveryPointDto dto = new()
        {
            Latitude = 45.0,
            Longitude = 90.0
        };
        UpdateDeliveryPointValidator validator = new();

        Result result = validator.Validate(dto);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
