namespace DistributionCenter.Services.Tests.Localization.Concretes;

using System.Net;
using Moq.Protected;
using Services.Localization.Concretes;

public class DistanceCalculatorTests
{
    [Fact]
    public async Task CalculateDistanceAsync_ValidResponse_ReturnsCorrectDistance()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;
        string jsonResponse =
            @"{
            ""routes"": [
                {
                    ""distance"": 75000
                }
            ]
        }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(
                () => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonResponse) }
            );

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DistanceCalculator distanceCalculator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        double result = await distanceCalculator.CalculateDistanceAsync(
            latitude,
            longitude,
            storeLatitude,
            storeLongitude
        );

        // Verify actual result
        Assert.Equal(75.0, result, 3);
    }

    [Fact]
    public async Task CalculateDistanceAsync_EmptyRoutesArray_ReturnsZero()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;
        string jsonResponse = @"{""routes"": []}";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(
                () => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonResponse) }
            );

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DistanceCalculator distanceCalculator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        double result = await distanceCalculator.CalculateDistanceAsync(
            latitude,
            longitude,
            storeLatitude,
            storeLongitude
        );

        // Verify actual result
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task CalculateDistanceAsync_HttpRequestFails_ReturnsZero()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -17.0;
        double storeLongitude = -68.0;

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.InternalServerError));

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DistanceCalculator distanceCalculator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        double result = await distanceCalculator.CalculateDistanceAsync(
            latitude,
            longitude,
            storeLatitude,
            storeLongitude
        );

        // Verify actual result
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task CalculateDistanceAsync_ZeroDistance_ReturnsZero()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = -16.5;
        double storeLongitude = -68.15;
        string jsonResponse =
            @"{
            ""routes"": [
                {
                    ""distance"": 0
                }
            ]
        }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(
                () => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonResponse) }
            );

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DistanceCalculator distanceCalculator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        double result = await distanceCalculator.CalculateDistanceAsync(
            latitude,
            longitude,
            storeLatitude,
            storeLongitude
        );

        // Verify actual result
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task CalculateDistanceAsync_VeryLargeDistance_ReturnsCorrectDistance()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        double storeLatitude = 40.7128;
        double storeLongitude = -74.0060;
        string jsonResponse =
            @"{
            ""routes"": [
                {
                    ""distance"": 7500000
                }
            ]
        }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(
                () => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonResponse) }
            );

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DistanceCalculator distanceCalculator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        double result = await distanceCalculator.CalculateDistanceAsync(
            latitude,
            longitude,
            storeLatitude,
            storeLongitude
        );

        // Verify actual result
        Assert.Equal(7500.0, result, 3);
    }
}
