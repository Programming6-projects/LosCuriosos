namespace DistributionCenter.Services.Localization.Interfaces;

using Commons;
using DistributionCenter.Commons.Results;
using Dtos;

public interface ILocationService
{
    Task<Result<LocationDto>> ProcessLocationDataAsync(GeoPoint sourcePoint, GeoPoint destinationPoint);
}
