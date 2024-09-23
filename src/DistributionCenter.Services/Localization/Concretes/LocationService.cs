namespace DistributionCenter.Services.Localization.Concretes;

using System.Threading.Tasks;
using Commons;
using DistributionCenter.Commons.Results;
using Dtos;
using Interfaces;

public class LocationService(ILocationValidator locationValidator, IDistanceCalculator distanceCalculator)
    : ILocationService
{
    private const int MinInCityDistanceKm = 35;
    public async Task<Result<LocationDto>> ProcessLocationDataAsync(GeoPoint sourcePoint, GeoPoint destinationPoint)
    {
        Result isInCountry = await locationValidator.IsLocationInCountryAsync(sourcePoint);

        if (!isInCountry.IsSuccess)
        {
            return isInCountry.Errors;
        }

        isInCountry = await locationValidator.IsLocationInCountryAsync(destinationPoint);

        if (!isInCountry.IsSuccess)
        {
            return isInCountry.Errors;
        }

        Result<double> distance = await distanceCalculator.CalculateDistanceAsync(sourcePoint, destinationPoint);

        if (!distance.IsSuccess)
        {
            return distance.Errors;
        }

        string category = distance.Value <= MinInCityDistanceKm ? "In City" : "Inter City";

        return new LocationDto(category, distance.Value);
    }
}
