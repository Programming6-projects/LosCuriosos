namespace DistributionCenter.Services.Localization.Concretes;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Interfaces;

public class DistanceCalculator(HttpClient httpClient) : IDistanceCalculator
{
    private const string AccessToken = "pk.eyJ1IjoibG9zY3VyaW9zb3MiLCJhIjoiY20weHliN251MGkxYTJrcHkycGZ6anpsMSJ9.3UziwOkzp-3mkrqPkp2wiw";
    private const string BaseUrl = "https://api.mapbox.com/";

    public async Task<double> CalculateDistanceAsync(double latitude, double longitude, double storeLatitude, double storeLongitude)
    {
        Uri url = new($"{BaseUrl}directions/v5/mapbox/driving/{storeLongitude},{storeLatitude};{longitude},{latitude}?access_token={AccessToken}&geometries=geojson");
        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return 0;
        }

        string json = await response.Content.ReadAsStringAsync();
        using JsonDocument directionData = JsonDocument.Parse(json);

        JsonElement routes = directionData.RootElement.GetProperty("routes");
        if (routes.GetArrayLength() == 0)
        {
            return 0;
        }

        double distanceInMeters = routes[0].GetProperty("distance").GetDouble();
        return distanceInMeters / 1000;
    }
}
