namespace DistributionCenter.Services.Tests.Routes.Dtos;

public class WayPointDtoTest
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        // Define Input and Output
        GeoPoint point = new (10, 10);
        int priority = 0;

        // Execute actual operation
        WayPointDto dto = new (point, priority);

        // Verify actual result
        Assert.Equal(point, dto.Point);
        Assert.Equal(priority, dto.Priority);
    }

    [Fact]
    public void GeoPoint_ReturnsCorrectValue()
    {
        // Define Input and Output
        GeoPoint point = new (30, 60);
        int priority = 1;

        // Execute actual operation
        WayPointDto dto = new (point, priority);

        // Verify actual result
        Assert.Equal(30, dto.Point.Latitude);
        Assert.Equal(60, dto.Point.Longitude);
    }
}
