namespace DistributionCenter.Services.Localization.Concretes;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Interfaces;

public class LocationValidator(HttpClient httpClient) : ILocationValidator
{
    private const string AccessToken = "pk.eyJ1IjoibG9zY3VyaW9zb3MiLCJhIjoiY20weHliN251MGkxYTJrcHkycGZ6anpsMSJ9.3UziwOkzp-3mkrqPkp2wiw";
    private const string BaseUrl = "https://api.mapbox.com/";

    public async Task<bool> IsLocationInCountryAsync(double latitude, double longitude)
    {
        Uri url = new($"{BaseUrl}geocoding/v5/mapbox.places/{longitude},{latitude}.json?access_token={AccessToken}");
        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        string json = await response.Content.ReadAsStringAsync();
        using JsonDocument locationData = JsonDocument.Parse(json);
        JsonElement features = locationData.RootElement.GetProperty("features");

        if (features.GetArrayLength() == 0)
        {
            return false;
        }

        JsonElement contextArray = features[0].GetProperty("context");

        if (contextArray.GetArrayLength() == 0)
        {
            return false;
        }

        JsonElement? country = contextArray.EnumerateArray()
            .FirstOrDefault(x => x.TryGetProperty("id", out JsonElement id) && id.GetString()?.StartsWith("country", StringComparison.Ordinal) == true);

        if (country.HasValue && country.Value.ValueKind != JsonValueKind.Undefined)
        {
            string? countryName = country.Value.GetProperty("text").GetString();
            return string.Equals(countryName, "Bolivia", StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }
}
