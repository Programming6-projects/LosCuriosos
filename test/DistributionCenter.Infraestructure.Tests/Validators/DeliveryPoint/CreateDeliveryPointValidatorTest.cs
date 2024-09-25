namespace DistributionCenter.Infraestructure.Tests.Validators.DeliveryPoint;

using Commons.Results;
using Infraestructure.DTOs.Concretes.DeliveryPoint;
using Infraestructure.Validators.Core.Concretes.DeliveryPoint;

public class CreateDeliveryPointValidatorTest
{
    [Fact]
    public void Constructor_ShouldInitializeWithoutExceptions()
    {
        CreateDeliveryPointValidator validator = new();

        // Assert
        Assert.NotNull(validator);
    }

    [Fact]
    public void Validate_ShouldPass_WhenLatitudeIsNotZero()
    {
        CreateDeliveryPointDto dto = new()
        {
            Latitude = 1.23,
            Longitude = 4.56
        };
        CreateDeliveryPointValidator validator = new();

        Result result = validator.Validate(dto);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Validate_ShouldPass_WhenLongitudeIsNotZero()
    {
        CreateDeliveryPointDto dto = new ()
        {
            Latitude = 1.23,
            Longitude = 4.56
        };
        CreateDeliveryPointValidator validator = new();

        Result result = validator.Validate(dto);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
