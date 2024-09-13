namespace DistributionCenter.Services.Localization.Concretes;

using System.Threading.Tasks;
using Dtos;
using Interfaces;

public class LocationService(ILocationValidator locationValidator, IDistanceCalculator distanceCalculator)
    : ILocationService
{
    public async Task<LocationDto> ProcessLocationDataAsync(double latitude, double longitude, double storeLatitude, double storeLongitude)
    {
        bool isInCountry = await locationValidator.IsLocationInCountryAsync(latitude, longitude);
        double distance = await distanceCalculator.CalculateDistanceAsync(latitude, longitude, storeLatitude, storeLongitude);
        string category = distance <= 35 ? "In City" : "Inter City";

        return new LocationDto(category, isInCountry, distance);
    }
}
