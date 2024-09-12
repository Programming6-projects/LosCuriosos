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
}
