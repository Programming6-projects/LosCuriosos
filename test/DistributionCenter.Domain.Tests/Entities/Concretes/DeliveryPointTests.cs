namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using Domain.Entities.Concretes;

public class DeliveryPointTests
{
    [Fact]
    public void Latitude_ShouldBeSetAndRetrieved()
    {
        // Arrange
        DeliveryPoint deliveryPoint = new ()
        {
            Latitude = 0,
            Longitude = 0
        };
        double expectedLatitude = 34.0522;

        deliveryPoint.Latitude = expectedLatitude;
        double actualLatitude = deliveryPoint.Latitude;

        // Assert
        Assert.Equal(expectedLatitude, actualLatitude);
    }

    [Fact]
    public void Longitude_ShouldBeSetAndRetrieved()
    {
        DeliveryPoint deliveryPoint = new ()
        {
            Latitude = 0,
            Longitude = 0
        };
        double expectedLongitude = -118.2437;

        deliveryPoint.Longitude = expectedLongitude;
        double actualLongitude = deliveryPoint.Longitude;

        // Assert
        Assert.Equal(expectedLongitude, actualLongitude);
    }
}
