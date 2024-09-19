namespace DistributionCenter.Services.Routes.Concretes;

using System.Text.Json;
using DistributionCenter.Services.Localization.Commons;
using DistributionCenter.Commons.Results;
using Commons.Errors;
using Dtos;
using Interfaces;

public class DeliveryRouteService(HttpClient httpClient, string mapboxAccessToken) : IRouteService
{
    private const string BaseUrl = "https://api.mapbox.com/optimized-trips/v1/mapbox/driving/";

    public async Task<Result<IReadOnlyList<WayPointDto>>> GetOptimalRoute(GeoPoint startPoint, IReadOnlyList<GeoPoint> geoPoints)
    {
        string url = BuildUrl(startPoint, geoPoints);
        HttpResponseMessage response = await SendRequest(url);

        if (!response.IsSuccessStatusCode)
        {
            return Error.Unauthorized(description: $"Invalid token or API request failed. Status code: {response.StatusCode}");
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        return ParseResponse(jsonResponse);
    }

    private string BuildUrl(GeoPoint startPoint, IReadOnlyList<GeoPoint> geoPoints)
    {
        List<GeoPoint> allPoints = new() { startPoint };
        allPoints.AddRange(geoPoints);

        string coordinates = string.Join(";", allPoints.Select(p => $"{p.Longitude},{p.Latitude}"));
        return $"{BaseUrl}{coordinates}?access_token={mapboxAccessToken}&geometries=geojson&source=first&destination=last";
    }

    private async Task<HttpResponseMessage> SendRequest(string url)
    {
        return await httpClient.GetAsync(new Uri(url));
    }

    private static Result<IReadOnlyList<WayPointDto>> ParseResponse(string jsonResponse)
    {
        using JsonDocument document = JsonDocument.Parse(jsonResponse);
        JsonElement root = document.RootElement;

        if (!root.TryGetProperty("code", out JsonElement codeElement))
        {
            return Error.Unexpected(description: $"Unexpected API response structure. Response: {jsonResponse}");
        }

        string? code = codeElement.GetString();
        if (code != "Ok")
        {
            return HandleNonOkStatus(root, code);
        }

        if (!root.TryGetProperty("waypoints", out JsonElement waypointsElement))
        {
            return Error.NotFound(description: $"No waypoints found in the response. Response: {jsonResponse}");
        }

        return ParseWaypoints(waypointsElement);
    }

    private static Result<IReadOnlyList<WayPointDto>> HandleNonOkStatus(JsonElement root, string? code)
    {
        string errorMessage = root.TryGetProperty("message", out JsonElement messageElement)
            ? messageElement.GetString() ?? "Unknown error"
            : "Unknown error";

        return Error.Unexpected(description: $"API returned non-OK status. Code: {code}, Message: {errorMessage}");
    }

    private static Result<IReadOnlyList<WayPointDto>> ParseWaypoints(JsonElement waypointsElement)
    {
        List<WayPointDto> waypoints = new();

        for (int i = 0; i < waypointsElement.GetArrayLength(); i++)
        {
            JsonElement waypoint = waypointsElement[i];

            if (!waypoint.TryGetProperty("location", out JsonElement locationElement) ||
                locationElement.GetArrayLength() < 2 ||
                !waypoint.TryGetProperty("waypoint_index", out JsonElement indexElement))
            {
                return Error.Unexpected(description: $"Invalid waypoint structure at index {i}. Waypoint: {waypoint}");
            }

            double longitude = locationElement[0].GetDouble();
            double latitude = locationElement[1].GetDouble();
            int waypointIndex = indexElement.GetInt32();

            GeoPoint point = new(latitude, longitude);
            WayPointDto waypointDto = new(point, waypointIndex);
            waypoints.Add(waypointDto);
        }

        waypoints = waypoints.OrderBy(w => w.Priority).ToList();

        return waypoints;
    }
}