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

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        GeoPoint point1 = new(10.0, 20.0);
        GeoPoint point2 = new(10.0, 20.0);

        Assert.True(point1.Equals(point2));
    }

    [Fact]
    public void Equals_DifferentValues_ReturnsFalse()
    {
        GeoPoint point1 = new(10.0, 20.0);
        GeoPoint point2 = new(10.0, 20.1);

        Assert.False(point1.Equals(point2));
    }

    [Fact]
    public void Equals_SlightlyDifferentValues_ReturnsTrue()
    {
        GeoPoint point1 = new(10.0, 20.0);
        GeoPoint point2 = new(10.0000000009, 20.0000000009);

        Assert.True(point1.Equals(point2));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        GeoPoint point = new(10.0, 20.0);
        object otherObject = new();

        Assert.False(point.Equals(otherObject));
    }

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        GeoPoint point1 = new(10.0, 20.0);
        GeoPoint point2 = new(10.0, 20.0);

        Assert.Equal(point1.GetHashCode(), point2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCodes()
    {
        GeoPoint point1 = new(10.0, 20.0);
        GeoPoint point2 = new(20.0, 10.0);

        Assert.NotEqual(point1.GetHashCode(), point2.GetHashCode());
    }

    [Theory]
    [InlineData(90.0, 180.0)]
    [InlineData(-90.0, -180.0)]
    [InlineData(0.0, 0.0)]
    public void Constructor_EdgeCases_SetsPropertiesCorrectly(double latitude, double longitude)
    {
        GeoPoint point = new(latitude, longitude);

        Assert.Equal(latitude, point.Latitude);
        Assert.Equal(longitude, point.Longitude);
    }

    [Fact]
    public void Equals_JustBeyondToleranceLimit_ReturnsFalse()
    {
        GeoPoint point1 = new(10.000000, 20.000000);
        GeoPoint point2 = new(10.0000011, 20.0000011);

        Assert.False(point1.Equals(point2));
    }

    [Fact]
    public void Equals_ToleranceAppliedIndependently_ReturnsExpectedResult()
    {
        GeoPoint point1 = new(10.000000, 20.000000);
        GeoPoint point2 = new(10.000001, 20.000002);

        Assert.False(point1.Equals(point2));
    }

    [Fact]
    public void Equals_VerySmallDifference_ReturnsTrue()
    {
        GeoPoint point1 = new(10.0000001, 20.0000001);
        GeoPoint point2 = new(10.0000002, 20.0000002);

        Assert.True(point1.Equals(point2));
    }

    [Theory]
    [InlineData(0.0000001)]
    [InlineData(0.0000005)]
    [InlineData(0.0000009)]
    public void Equals_VariousSmallDifferences_ReturnsTrue(double difference)
    {
        GeoPoint point1 = new(10.000000, 20.000000);
        GeoPoint point2 = new(10.000000 + difference, 20.000000 + difference);

        Assert.True(point1.Equals(point2));
    }
}
