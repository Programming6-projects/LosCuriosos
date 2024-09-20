namespace DistributionCenter.Services.Localization.Concretes;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Commons;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using Interfaces;

public class LocationValidator(HttpClient httpClient, string mapboxAccessToken) : ILocationValidator
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _accessToken = mapboxAccessToken;
    private const string BaseUrl = "https://api.mapbox.com/";

    public async Task<Result> IsLocationInCountryAsync(GeoPoint point)
    {
        Uri url = new($"{BaseUrl}geocoding/v5/mapbox.places/{point.Longitude},{point.Latitude}.json?access_token={_accessToken}");

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return Error.Unauthorized(description: "The provided access token is invalid or expired.");
        }

        string json = await response.Content.ReadAsStringAsync();
        using JsonDocument locationData = JsonDocument.Parse(json);
        JsonElement features = locationData.RootElement.GetProperty("features");

        if (features.GetArrayLength() == 0)
        {
            return Error.NotFound(description: "No features found in the geocoding response.");
        }

        JsonElement contextArray = features[0].GetProperty("context");

        if (contextArray.GetArrayLength() == 0)
        {
            return Error.NotFound(description: "No context information found in the features array.");
        }

        JsonElement? country = contextArray
            .EnumerateArray()
            .FirstOrDefault(static x =>
                x.TryGetProperty("id", out JsonElement id)
                && id.GetString()?.StartsWith("country", StringComparison.Ordinal) == true
            );

        if (country.HasValue)
        {
            if (country.Value.ValueKind != JsonValueKind.Undefined)
            {
                string? countryName = country.Value.GetProperty("text").GetString();
                if (string.Equals(countryName, "Bolivia", StringComparison.OrdinalIgnoreCase))
                {
                    return Result.Ok();
                }
                return Error.Unexpected(description: "The identified country is not Bolivia.");
            }
        }
        return Error.Unexpected(description: "Country property is defined but has an 'undefined' value.");
    }
}
