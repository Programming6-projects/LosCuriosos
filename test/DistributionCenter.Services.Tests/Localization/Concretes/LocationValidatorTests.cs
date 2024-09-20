namespace DistributionCenter.Services.Tests.Localization.Concretes;

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Commons.Results;
using DistributionCenter.Services.Localization.Concretes;
using Moq.Protected;
using Services.Localization.Commons;

public class LocationValidatorTests
{
    [Fact]
    public async Task IsLocationInCountryAsync_LocationInBolivia_ReturnsOkResult()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
                ""features"": [{
                    ""context"": [
                        { ""id"": ""country.9903"", ""text"": ""Bolivia"" }
                    ]
                }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_LocationNotInBolivia_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
                ""features"": [{
                    ""context"": [
                        { ""id"": ""country.3686110"", ""text"": ""Colombia"" }
                    ]
                }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("The identified country is not Bolivia.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_NoFeaturesInResponse_ReturnsNotFoundError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse = @"{ ""features"": [] }";

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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("No features found in the geocoding response.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_NoContextInFeatures_ReturnsNotFoundError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse = @"{ ""features"": [{ ""context"": [] }] }";

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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("No context information found in the features array.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_ApiFailure_ReturnsUnauthorizedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);

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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("The provided access token is invalid or expired.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_CountryPropertyMissing_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
                ""features"": [{
                    ""context"": [
                        { ""id"": ""city:LaPaz"", ""text"": ""La Paz"" }
                    ]
                }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Country property is defined but has an 'undefined' value.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_CountryIdStartsWithCountry_ReturnsOkResult()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
                ""features"": [{
                    ""context"": [
                        { ""id"": ""country.9903"", ""text"": ""Bolivia"" }
                    ]
                }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_CountryIdDoesNotStartWithCountry_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
                ""features"": [{
                    ""context"": [
                        { ""id"": ""city.1234"", ""text"": ""La Paz"" }
                    ]
                }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Country property is defined but has an 'undefined' value.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_MissingIdProperty_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
            ""features"": [{
                ""context"": [
                    { ""text"": ""Bolivia"" }
                ]
            }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Country property is defined but has an 'undefined' value.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_CountryValueIsUndefined_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
            ""features"": [{
                ""context"": [
                    { ""id"": ""country.9903"", ""text"": null }
                ]
            }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("The identified country is not Bolivia.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_IdPropertyPresentAndCorrect_ReturnsOkResult()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
            ""features"": [{
                ""context"": [
                    { ""id"": ""country.9903"", ""text"": ""Bolivia"" }
                ]
            }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_IdPropertyPresentButDoesNotStartWithCountry_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
            ""features"": [{
                ""context"": [
                    { ""id"": ""city.1234"", ""text"": ""La Paz"" }
                ]
            }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Country property is defined but has an 'undefined' value.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_IdPropertyMissing_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
            ""features"": [{
                ""context"": [
                    { ""text"": ""Bolivia"" }
                ]
            }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Country property is defined but has an 'undefined' value.", result.Errors[0].Description);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_IdPropertyIsNull_ReturnsUnexpectedError()
    {
        // Define Input and Output
        GeoPoint point = new(-16.5, -68.15);
        string jsonResponse =
            @"{
            ""features"": [{
                ""context"": [
                    { ""id"": null, ""text"": ""Bolivia"" }
                ]
            }]
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
        LocationValidator locationValidator = new(httpClient, "MapboxAccessToken");

        // Execute actual operation
        Result result = await locationValidator.IsLocationInCountryAsync(point);

        // Verify actual result
        Assert.False(result.IsSuccess);
        Assert.Equal("Country property is defined but has an 'undefined' value.", result.Errors[0].Description);
    }
}
