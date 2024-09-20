namespace DistributionCenter.Services.Tests.Localization.CommonsTests;

public class GeoPointTest
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Define Input and Output
        double expectedLatitude = -16.5;
        double expectedLongitude = -68.15;

        // Execute actual operation
        GeoPoint geoPoint = new(expectedLatitude, expectedLongitude);

        // Verify actual result
        Assert.Equal(expectedLatitude, geoPoint.Latitude);
        Assert.Equal(expectedLongitude, geoPoint.Longitude);
    }

    [Fact]
    public void Properties_SetAndGetValuesCorrectly()
    {
        // Define Input and Output
        double initialLatitude = 0.0;
        double initialLongitude = 0.0;
        double newLatitude = 45.0;
        double newLongitude = 90.0;

        GeoPoint geoPoint = new(initialLatitude, initialLongitude);

        // Execute actual operation
        geoPoint.Latitude = newLatitude;
        geoPoint.Longitude = newLongitude;

        // Verify actual result
        Assert.Equal(newLatitude, geoPoint.Latitude);
        Assert.Equal(newLongitude, geoPoint.Longitude);
    }

    [Fact]
    public void Constructor_DefaultValues_AreSet()
    {
        // Define Input and Output
        double latitude = 0.0;
        double longitude = 0.0;

        // Execute actual operation
        GeoPoint geoPoint = new(latitude, longitude);

        // Verify actual result
        Assert.Equal(latitude, geoPoint.Latitude);
        Assert.Equal(longitude, geoPoint.Longitude);
    }
}
