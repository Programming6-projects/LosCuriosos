namespace DistributionCenter.Services.Tests.Routes.Concretes;

using System.Net;
using System.Text.Json;
using Commons.Results;
using Moq.Protected;

public class DeliveryRouteServiceTest
{
    [Fact]
    public async Task GetOptimalRoute_ValidResponse_ReturnsCorrectWaypoints()
    {
        // Define Input and Output
        GeoPoint startPoint = new(0.0, 0.0);
        IReadOnlyList<GeoPoint> geoPoints = new List<GeoPoint>
        {
            new (1.0, 1.0),
            new (2.0, 2.0)
        };
        string jsonResponse =
            @"{
                ""code"": ""Ok"",
                ""waypoints"": [
                    { ""location"": [0.0, 0.0], ""waypoint_index"": 0 },
                    { ""location"": [1.0, 1.0], ""waypoint_index"": 1 },
                    { ""location"": [2.0, 2.0], ""waypoint_index"": 2 }
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
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonResponse) });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DeliveryRouteService deliveryRouteService = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await deliveryRouteService.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value.Count);
        Assert.Equal(0, result.Value[0].Priority);
        Assert.Equal(1, result.Value[1].Priority);
        Assert.Equal(2, result.Value[2].Priority);
    }

    [Fact]
    public async Task GetOptimalRoute_HttpRequestFails_ReturnsUnauthorizedError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(0.0, 0.0);
        IReadOnlyList<GeoPoint> geoPoints = new List<GeoPoint>
        {
            new (1.0, 1.0),
            new (2.0, 2.0)
        };

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
        DeliveryRouteService deliveryRouteService = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await deliveryRouteService.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid token or API request failed. Status code: Unauthorized", result.Errors[0].Description);
    }

    [Fact]
    public async Task GetOptimalRoute_NonOkResponse_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(0.0, 0.0);
        IReadOnlyList<GeoPoint> geoPoints = new List<GeoPoint>
        {
            new (1.0, 1.0),
            new (2.0, 2.0)
        };
        string jsonResponse =
            @"{
                ""code"": ""Error"",
                ""message"": ""Invalid request""
            }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonResponse) });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        DeliveryRouteService deliveryRouteService = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await deliveryRouteService.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("API returned non-OK status. Code: Error, Message: Invalid request", result.Errors[0].Description);
    }
    [Fact]
    public async Task GetOptimalRoute_NoCodeProperty_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(10.0, 20.0);
        GeoPoint[] geoPoints = { new(30.0, 40.0), new(50.0, 60.0) };

        string jsonResponse = @"{ ""wrong_property"": ""some_value"" }";

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
        DeliveryRouteService service = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await service.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Unexpected API response structure. Response: { \"wrong_property\": \"some_value\" }", result.Errors[0].Description);
    }

    [Fact]
    public async Task GetOptimalRoute_NoWaypointsProperty_ReturnsNotFoundError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(10.0, 20.0);
        GeoPoint[] geoPoints = { new(30.0, 40.0), new(50.0, 60.0) };

        string jsonResponse = @"{ ""code"": ""Ok"", ""wrong_property"": ""some_value"" }";

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
        DeliveryRouteService service = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await service.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("No waypoints found in the response. Response: { \"code\": \"Ok\", \"wrong_property\": \"some_value\" }", result.Errors[0].Description);
    }

    [Fact]
    public async Task GetOptimalRoute_NonOkStatus_NoMessageProperty_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(10.0, 20.0);
        GeoPoint[] geoPoints = { new(30.0, 40.0), new(50.0, 60.0) };

        // Simulate API response with "code" set to a non-OK value and without "message" property
        string jsonResponse = @"{ ""code"": ""ErrorCode"", ""wrong_property"": ""some_value"" }";

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
        DeliveryRouteService service = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await service.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("API returned non-OK status. Code: ErrorCode, Message: Unknown error", result.Errors[0].Description);
    }

    [Fact]
    public async Task GetOptimalRoute_InvalidWaypointStructure_MissingLocation_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(10.0, 20.0);
        GeoPoint[] geoPoints = { new(30.0, 40.0), new(50.0, 60.0) };

        // Simulate API response with missing "location" property
        string jsonResponse = @"{ ""code"": ""Ok"", ""waypoints"": [ { ""waypoint_index"": 0 } ] }";

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
        DeliveryRouteService service = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await service.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid waypoint structure at index 0. Waypoint: { \"waypoint_index\": 0 }", result.Errors[0].Description);
    }

    [Fact]
    public async Task GetOptimalRoute_InvalidWaypointStructure_LocationTooShort_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(10.0, 20.0);
        GeoPoint[] geoPoints = { new(30.0, 40.0), new(50.0, 60.0) };

        // Simulate API response with "location" property containing less than 2 elements
        string jsonResponse = @"{ ""code"": ""Ok"", ""waypoints"": [ { ""location"": [10.0], ""waypoint_index"": 0 } ] }";

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
        DeliveryRouteService service = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await service.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid waypoint structure at index 0. Waypoint: { \"location\": [10.0], \"waypoint_index\": 0 }", result.Errors[0].Description);
    }

    [Fact]
    public async Task GetOptimalRoute_InvalidWaypointStructure_MissingWaypointIndex_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint startPoint = new(10.0, 20.0);
        GeoPoint[] geoPoints = { new(30.0, 40.0), new(50.0, 60.0) };

        // Simulate API response with missing "waypoint_index" property
        string jsonResponse = @"{ ""code"": ""Ok"", ""waypoints"": [ { ""location"": [10.0, 20.0] } ] }";

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
        DeliveryRouteService service = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result<IReadOnlyList<WayPointDto>> result = await service.GetOptimalRoute(startPoint, geoPoints);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid waypoint structure at index 0. Waypoint: { \"location\": [10.0, 20.0] }", result.Errors[0].Description);
    }

    [Fact]
    public void HandleNonOkStatus_MessagePropertyPresent_ReturnsMessage()
    {
        // Simulate JSON with "message" property containing a non-null string
        string jsonResponse = @"{ ""message"": ""Error occurred"" }";
        JsonDocument doc = JsonDocument.Parse(jsonResponse);
        JsonElement root = doc.RootElement;

        string errorMessage = root.TryGetProperty("message", out JsonElement messageElement)
            ? messageElement.GetString() ?? "Unknown error"
            : "Unknown error";

        Assert.Equal("Error occurred", errorMessage);
    }
    [Fact]
    public void HandleNonOkStatus_MessagePropertyPresentButNull_ReturnsDefaultErrorMessage()
    {
        // Simulate JSON with "message" property set to null
        string jsonResponse = @"{ ""message"": null }";
        JsonDocument doc = JsonDocument.Parse(jsonResponse);
        JsonElement root = doc.RootElement;

        string errorMessage = root.TryGetProperty("message", out JsonElement messageElement)
            ? messageElement.GetString() ?? "Unknown error"
            : "Unknown error";

        Assert.Equal("Unknown error", errorMessage);
    }

    [Fact]
    public void HandleNonOkStatus_MessagePropertyAbsent_ReturnsDefaultErrorMessage()
    {
        // Simulate JSON without the "message" property
        string jsonResponse = @"{ }";
        JsonDocument doc = JsonDocument.Parse(jsonResponse);
        JsonElement root = doc.RootElement;

        string errorMessage = root.TryGetProperty("message", out JsonElement messageElement)
            ? messageElement.GetString() ?? "Unknown error"
            : "Unknown error";

        Assert.Equal("Unknown error", errorMessage);
    }
}
