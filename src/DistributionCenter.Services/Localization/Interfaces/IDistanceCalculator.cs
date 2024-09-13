namespace DistributionCenter.Services.Localization.Interfaces;

using System.Threading.Tasks;

public interface IDistanceCalculator
{
    Task<double> CalculateDistanceAsync(double latitude, double longitude, double storeLatitude, double storeLongitude);
}
