namespace DistributionCenter.Services.Tests.Localization.Concretes;

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Commons.Results;
using DistributionCenter.Services.Localization.Concretes;
using Moq.Protected;
using Services.Localization.Commons;

public class DistanceCalculatorTests
{
    [Fact]
    public async Task CalculateDistanceAsync_ValidResponse_ReturnsCorrectDistance()
    {
        // Define Input and Output
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);
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
        Result<double> result = await distanceCalculator.CalculateDistanceAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(75.0, result.Value, 3);
    }

    [Fact]
    public async Task CalculateDistanceAsync_EmptyRoutesArray_ReturnsNotFoundError()
    {
        // Define Input and Output
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);
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
        Result<double> result = await distanceCalculator.CalculateDistanceAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("routes not found", result.Errors[0].Description);
    }

    [Fact]
    public async Task CalculateDistanceAsync_HttpRequestFails_ReturnsUnauthorizedError()
    {
        // Define Input and Output
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-17.0, -68.0);

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.Unauthorized));

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DistanceCalculator distanceCalculator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<double> result = await distanceCalculator.CalculateDistanceAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("invalid token", result.Errors[0].Description);
    }

    [Fact]
    public async Task CalculateDistanceAsync_ZeroDistance_ReturnsZero()
    {
        // Define Input and Output
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(-16.5, -68.15);
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
        Result<double> result = await distanceCalculator.CalculateDistanceAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public async Task CalculateDistanceAsync_VeryLargeDistance_ReturnsCorrectDistance()
    {
        // Define Input and Output
        GeoPoint sourcePoint = new(-16.5, -68.15);
        GeoPoint destinationPoint = new(40.7128, -74.0060);
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
        Result<double> result = await distanceCalculator.CalculateDistanceAsync(sourcePoint, destinationPoint);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(7500.0, result.Value, 3);
    }
}
