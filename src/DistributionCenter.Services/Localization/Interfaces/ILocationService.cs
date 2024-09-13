namespace DistributionCenter.Services.Localization.Interfaces;

using Dtos;

public interface ILocationService
{
    Task<LocationDto> ProcessLocationDataAsync(double latitude, double longitude, double storeLatitude, double storeLongitude);
}
