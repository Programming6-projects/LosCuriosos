namespace DistributionCenter.Services.Tests.Localization.Concretes;

using System.Net;
using System.Net.Http;
using Moq;
using Moq.Protected;
using Xunit;
using DistributionCenter.Services.Localization.Concretes;

public class LocationValidatorTests
{
    [Fact]
    public async Task IsLocationInCountryAsync_LocationInBolivia_ReturnsTrue()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        string jsonResponse = @"{
            ""features"": [{
                ""context"": [
                    { ""id"": ""country:BO"", ""text"": ""Bolivia"" }
                ]
            }]
        }";
        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        LocationValidator locationValidator = new(httpClient);

        // Execute actual operation
        bool result = await locationValidator.IsLocationInCountryAsync(latitude, longitude);

        // Verify actual result
        Assert.True(result);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_LocationNotInBolivia_ReturnsFalse()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        string jsonResponse = @"{
            ""features"": [{
                ""context"": [
                    { ""id"": ""country:CO"", ""text"": ""Colombia"" }
                ]
            }]
        }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        LocationValidator locationValidator = new(httpClient);

        // Execute actual operation
        bool result = await locationValidator.IsLocationInCountryAsync(latitude, longitude);

        // Verify actual result
        Assert.False(result);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_NoFeaturesInResponse_ReturnsFalse()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        string jsonResponse = @"{
            ""features"": []
        }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        LocationValidator locationValidator = new(httpClient);

        // Execute actual operation
        bool result = await locationValidator.IsLocationInCountryAsync(latitude, longitude);

        // Verify actual result
        Assert.False(result);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_NoContextInFeatures_ReturnsFalse()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        string jsonResponse = @"{
            ""features"": [{
                ""context"": []
            }]
        }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        LocationValidator locationValidator = new(httpClient);

        // Execute actual operation
        bool result = await locationValidator.IsLocationInCountryAsync(latitude, longitude);

        // Verify actual result
        Assert.False(result);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_ApiFailure_ReturnsFalse()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.BadRequest));

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        LocationValidator locationValidator = new(httpClient);

        // Execute actual operation
        bool result = await locationValidator.IsLocationInCountryAsync(latitude, longitude);

        // Verify actual result
        Assert.False(result);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_CountryPropertyMissing_ReturnsFalse()
    {
        // Define input and output
        double latitude = -16.5;
        double longitude = -68.15;
        string jsonResponse = @"{
            ""features"": [{
                ""context"": [
                    { ""id"": ""city:LaPaz"", ""text"": ""La Paz"" }
                ]
            }]
        }"; // Missing "country" in context

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        LocationValidator locationValidator = new(httpClient);

        // Execute actual operation
        bool result = await locationValidator.IsLocationInCountryAsync(latitude, longitude);

        // Verify result
        Assert.False(result);
    }

    [Fact]
    public async Task IsLocationInCountryAsync_LocationInSpecifiedCountry_ReturnsFalse()
    {
        // Define Input and Output
        double latitude = -16.5;
        double longitude = -68.15;
        string jsonResponse = @"{
            ""features"": [{
                ""context"": [
                    { ""id"": ""country:US"", ""text"": ""United States"" }
                ]
            }]
        }";

        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        _ = mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            });

        using HttpClient httpClient = new(mockHttpMessageHandler.Object);
        LocationValidator locationValidator = new(httpClient);

        // Execute actual operation
        bool result = await locationValidator.IsLocationInCountryAsync(latitude, longitude);

        // Verify actual result
        Assert.False(result);
    }
}
