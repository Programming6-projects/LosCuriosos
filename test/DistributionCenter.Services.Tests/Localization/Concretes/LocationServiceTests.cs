namespace DistributionCenter.Services.Tests.Localization.Concretes;

using DistributionCenter.Services.Localization.Concretes;
using DistributionCenter.Services.Localization.Dtos;
using DistributionCenter.Services.Localization.Interfaces;
using DistributionCenter.Services.Localization.Commons;
using Commons.Results;
using Commons.Errors;

public class LocationServiceTests
{
    [Fact]
    public async Task ProcessLocationDataAsync_LocationInCity_ReturnsCorrectDto()
    {
        // Define Input and Output
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);
        double expectedDistance = 25.0;
        string expectedCategory = "In City";

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(It.IsAny<GeoPoint>()))
            .ReturnsAsync(Result.Ok());

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(sourcePoint, destinationPoint))
            .ReturnsAsync(expectedDistance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        Result<LocationDto> result = await locationService.ProcessLocationDataAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedCategory, result.Value.Category);
        Assert.Equal(expectedDistance, result.Value.DistanceFromStore);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_LocationInterCity_ReturnsCorrectDto()
    {
        // Define Input and Output
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);
        double expectedDistance = 75.0;
        string expectedCategory = "Inter City";

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(It.IsAny<GeoPoint>()))
            .ReturnsAsync(Result.Ok());

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(sourcePoint, destinationPoint))
            .ReturnsAsync(expectedDistance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        Result<LocationDto> result = await locationService.ProcessLocationDataAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedCategory, result.Value.Category);
        Assert.Equal(expectedDistance, result.Value.DistanceFromStore);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_SourceLocationNotInCountry_ReturnsError()
    {
        // Define Input
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(sourcePoint))
            .ReturnsAsync(Error.Validation("Location.NotInCountry", "Source location is not in the country"));

        Mock<IDistanceCalculator> mockDistanceCalculator = new();

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        Result<LocationDto> result = await locationService.ProcessLocationDataAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.False(result.IsSuccess);
        _ = Assert.Single(result.Errors);
        Assert.Equal("Location.NotInCountry", result.Errors[0].Code);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_DestinationLocationNotInCountry_ReturnsError()
    {
        // Define Input
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(sourcePoint))
            .ReturnsAsync(Result.Ok());
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(destinationPoint))
            .ReturnsAsync(Error.Validation("Location.NotInCountry", "Destination location is not in the country"));

        Mock<IDistanceCalculator> mockDistanceCalculator = new();

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        Result<LocationDto> result = await locationService.ProcessLocationDataAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.False(result.IsSuccess);
        _ = Assert.Single(result.Errors);
        Assert.Equal("Location.NotInCountry", result.Errors[0].Code);
    }

    [Fact]
    public async Task ProcessLocationDataAsync_DistanceCalculationFails_ReturnsError()
    {
        // Define Input
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(It.IsAny<GeoPoint>()))
            .ReturnsAsync(Result.Ok());

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(sourcePoint, destinationPoint))
            .ReturnsAsync (Error.Unexpected("Distance.CalculationFailed", "Failed to calculate distance"));

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        Result<LocationDto> result = await locationService.ProcessLocationDataAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.False(result.IsSuccess);
        _ = Assert.Single(result.Errors);
        Assert.Equal("Distance.CalculationFailed", result.Errors[0].Code);
    }

    [Theory]
    [InlineData(35.0, "In City")]
    [InlineData(35.1, "Inter City")]
    public async Task ProcessLocationDataAsync_DistanceThreshold_ReturnsCorrectCategory(double distance, string expectedCategory)
    {
        // Define Input
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);

        // Setup Mock Dependencies
        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(lv => lv.IsLocationInCountryAsync(It.IsAny<GeoPoint>()))
            .ReturnsAsync(Result.Ok());

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(dc => dc.CalculateDistanceAsync(sourcePoint, destinationPoint))
            .ReturnsAsync(distance);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Execute actual operation
        Result<LocationDto> result = await locationService.ProcessLocationDataAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedCategory, result.Value.Category);
        Assert.Equal(distance, result.Value.DistanceFromStore);
    }

    [Theory]
    [InlineData(double.MinValue, double.MinValue, double.MaxValue, double.MaxValue)]
    [InlineData(double.MaxValue, double.MaxValue, double.MinValue, double.MinValue)]
    public async Task ProcessLocationDataAsync_ExtremeCoordinates_HandlesCorrectly(double lat1, double lon1, double lat2, double lon2)
    {
        // Arrange
        GeoPoint sourcePoint = new(lat1, lon1);
        GeoPoint destinationPoint = new(lat2, lon2);

        Mock<ILocationValidator> mockLocationValidator = new();
        _ = mockLocationValidator
            .Setup(x => x.IsLocationInCountryAsync(It.IsAny<GeoPoint>()))
            .ReturnsAsync(Result.Ok());

        Mock<IDistanceCalculator> mockDistanceCalculator = new();
        _ = mockDistanceCalculator
            .Setup(x => x.CalculateDistanceAsync(sourcePoint, destinationPoint))
            .ReturnsAsync(100.0);

        LocationService locationService = new(mockLocationValidator.Object, mockDistanceCalculator.Object);

        // Act
        Result<LocationDto> result = await locationService.ProcessLocationDataAsync(sourcePoint, destinationPoint);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Inter City", result.Value.Category);
        Assert.Equal(100.0, result.Value.DistanceFromStore);
    }
}
