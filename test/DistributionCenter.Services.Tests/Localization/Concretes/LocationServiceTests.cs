namespace DistributionCenter.Services.Tests.Localization.Concretes;

using Services.Localization.Concretes;
using Services.Localization.Dtos;
using Services.Localization.Interfaces;

public class LocationServiceTests
{
    [Fact]
    public async Task ValidateAndCategorizeLocationAsync_LocationInCityAndInCountry_ReturnsCorrectDto()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;
        bool expectedIsInCountry = true;
        double expectedDistance = 25.0;
        string expectedCategory = "In City";

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(expectedIsInCountry);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(expectedDistance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify actual result
        Assert.Equal(expectedCategory, result.Category);
        Assert.Equal(expectedIsInCountry, result.IsInCountry);
        Assert.Equal(expectedDistance, result.DistanceFromStore);
    }

    [Fact]
    public async Task ValidateAndCategorizeLocationAsync_LocationInterCityAndInCountry_ReturnsCorrectDto()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;
        bool expectedIsInCountry = true;
        double expectedDistance = 75.0;
        string expectedCategory = "Inter City";

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(expectedIsInCountry);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(expectedDistance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify actual result
        Assert.Equal(expectedCategory, result.Category);
        Assert.Equal(expectedIsInCountry, result.IsInCountry);
        Assert.Equal(expectedDistance, result.DistanceFromStore);
    }

    [Fact]
    public async Task ValidateAndCategorizeLocationAsync_LocationNotInCountry_ReturnsCorrectDto()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;
        bool expectedIsInCountry = false;
        double expectedDistance = 75.0;
        string expectedCategory = "Inter City";

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(expectedIsInCountry);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(expectedDistance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify actual result
        Assert.Equal(expectedCategory, result.Category);
        Assert.Equal(expectedIsInCountry, result.IsInCountry);
        Assert.Equal(expectedDistance, result.DistanceFromStore);
    }

    [Fact]
    public async Task ValidateAndCategorizeLocationAsync_ShortDistance_ReturnsInCityCategory()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;
        bool expectedIsInCountry = true;
        double expectedDistance = 10.0;
        string expectedCategory = "In City";

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(expectedIsInCountry);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(expectedDistance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify actual result
        Assert.Equal(expectedCategory, result.Category);
        Assert.Equal(expectedIsInCountry, result.IsInCountry);
        Assert.Equal(expectedDistance, result.DistanceFromStore);
    }

    [Fact]
    public async Task ValidateAndCategorizeLocationAsync_LongDistance_ReturnsInterCityCategory()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;
        bool expectedIsInCountry = true;
        double expectedDistance = 100.0;
        string expectedCategory = "Inter City";

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(expectedIsInCountry);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(expectedDistance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify actual result
        Assert.Equal(expectedCategory, result.Category);
        Assert.Equal(expectedIsInCountry, result.IsInCountry);
        Assert.Equal(expectedDistance, result.DistanceFromStore);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_LocationInCountryAndInCity_ReturnsInCityCategory()
    {
        // Define input
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -16.5;
        double storeLongitude = -68.15;

        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator.Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(true);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator.Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(10);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify result
        Assert.Equal("In City", result.Category);
        Assert.True(result.IsInCountry);
        Assert.Equal(10, result.DistanceFromStore);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_LocationInCountryAndInterCity_ReturnsInterCityCategory()
    {
        // Define input
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -16.5;
        double storeLongitude = -67.9;
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator.Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(true);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator.Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(50);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify result
        Assert.Equal("Inter City", result.Category);
        Assert.True(result.IsInCountry);
        Assert.Equal(50, result.DistanceFromStore);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_LocationNotInCountry_ReturnsCorrectCategoryAndIsInCountryFalse()
    {
        // Define input
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -16.5;
        double storeLongitude = -68.15;

        // Mock ILocationValidator
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator.Setup(lv => lv.IsLocationInCountryAsync(latitude, longitude))
            .ReturnsAsync(false);

        // Mock IDistanceCalculator
        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator.Setup(dc => dc.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude))
            .ReturnsAsync(15);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        LocationDto result = await locationService.ProcessLocationDataAsync(latitude, longitude, storeLatitude, storeLongitude);

        // Verify result
        Assert.Equal("In City", result.Category);
        Assert.False(result.IsInCountry);
        Assert.Equal(15, result.DistanceFromStore);
    }

    [Theory]
    [InlineData(double.MinValue, double.MinValue, double.MaxValue, double.MaxValue)]
    [InlineData(double.MaxValue, double.MaxValue, double.MinValue, double.MinValue)]
    public async Task ProcessLocationDataAsync_ExtremeCoordinates_HandlesCorrectly(double lat1, double lon1, double lat2, double lon2)
    {
        // Arrange
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(x => x.IsLocationInCountryAsync(It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(true);

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(x => x.CalculateDistanceAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(100.0);

        LocationService locationService = new (mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Act
        LocationDto result = await locationService.ProcessLocationDataAsync(lat1, lon1, lat2, lon2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Inter City", result.Category);
        Assert.True(result.IsInCountry);
        Assert.Equal(100.0, result.DistanceFromStore);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_ExactlyOnDistanceThreshold_ReturnsCorrectCategory()
    {
        // Arrange
        Mock<ILocationValidator> mockLocationValidator = new ();
        _ = mockLocationValidator
            .Setup(x => x.IsLocationInCountryAsync(It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(true);

        Mock<IDistanceCalculator> mockDistanceCalculator = new ();
        _ = mockDistanceCalculator
            .Setup(x => x.CalculateDistanceAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(50.0); // Assuming 50 is the threshold between "In City" and "Inter City"

        LocationService locationService = new (mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Act
        LocationDto result = await locationService.ProcessLocationDataAsync(0, 0, 1, 1);

        // Assert
        Assert.Equal("Inter City", result.Category);
        Assert.True(result.IsInCountry);
        Assert.Equal(50.0, result.DistanceFromStore);
    }
}
