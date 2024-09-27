namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.DeliveryPoint;

using Commons.Results;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.DeliveryPoint;

public class CreateDeliveryPointDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectDeliveryPoint()
    {
        // Define Input and Output
        CreateDeliveryPointDto dto =
            new()
            {
                Latitude = 0,
                Longitude = 0,
            };

        // Execute actual operation
        DeliveryPoint deliveryPoint = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.Latitude, deliveryPoint.Latitude);
        Assert.Equal(dto.Longitude, deliveryPoint.Longitude);
    }

    [Fact]
    public void Latitude_ShouldBeSetAndRetrieved()
    {
        CreateDeliveryPointDto dto = new ()
        {
            Latitude = 0,
            Longitude = 0
        };
        double expectedLatitude = 34.0522;

        dto.Latitude = expectedLatitude;
        double actualLatitude = dto.Latitude;

        // Assert
        Assert.Equal(expectedLatitude, actualLatitude);
    }

    [Fact]
    public void Longitude_ShouldBeSetAndRetrieved()
    {
        CreateDeliveryPointDto dto = new ()
        {
            Latitude = 0,
            Longitude = 0
        };
        double expectedLongitude = -118.2437;

        dto.Longitude = expectedLongitude;
        double actualLongitude = dto.Longitude;

        // Assert
        Assert.Equal(expectedLongitude, actualLongitude);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult()
    {
        CreateDeliveryPointDto dto = new ()
        {
            Latitude = 34.0522,
            Longitude = -118.2437
        };

        Result result = dto.Validate();

        // Assert
        Assert.True(result.IsSuccess);
    }
}
