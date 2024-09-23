namespace DistributionCenter.Services.Routes.Dtos;

using Localization.Commons;

public class WayPointDto (GeoPoint point, int priority)
{
    public GeoPoint Point { get; set; } = point;
    public int Priority { get; set; } = priority;
}
