namespace DistributionCenter.Services.Routes.Interfaces;

using Commons.Results;
using Dtos;
using Localization.Commons;

public interface IRouteService
{
    Task<Result<IReadOnlyList<WayPointDto>>> GetOptimalRoute(GeoPoint startPoint, IReadOnlyList<GeoPoint> geoPoints, DateTime startTime);
}
