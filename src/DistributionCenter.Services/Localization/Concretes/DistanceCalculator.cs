namespace DistributionCenter.Services.Localization.Concretes;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Commons;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Commons.Results;
using Interfaces;

public class DistanceCalculator(HttpClient httpClient, string mapboxAccessToken) : IDistanceCalculator
{
    private const string BaseUrl = "https://api.mapbox.com/";
    private const int MetersToKilometers = 1000;

    public async Task<Result<double>> CalculateDistanceAsync(GeoPoint sourcePoint, GeoPoint destinationPoint)
    {
        Uri url =
            new(
                $"{BaseUrl}directions/v5/mapbox/driving/{sourcePoint.Longitude},{sourcePoint.Latitude};" +
                $"{destinationPoint.Longitude},{destinationPoint.Latitude}?access_token={mapboxAccessToken}&geometries=geojson"
            );

        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return Error.Unauthorized(description: "invalid token");
        }

        string json = await response.Content.ReadAsStringAsync();
        using JsonDocument directionData = JsonDocument.Parse(json);

        JsonElement routes = directionData.RootElement.GetProperty("routes");
        if (routes.GetArrayLength() == 0)
        {
            return Error.NotFound(description: "routes not found");
        }

        double distanceInMeters = routes[0].GetProperty("distance").GetDouble();
        return distanceInMeters / MetersToKilometers;
    }
}
