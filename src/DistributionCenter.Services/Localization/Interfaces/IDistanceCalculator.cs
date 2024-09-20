namespace DistributionCenter.Services.Localization.Interfaces;

using System.Threading.Tasks;
using Commons;
using DistributionCenter.Commons.Results;

public interface IDistanceCalculator
{
    Task<Result<double>> CalculateDistanceAsync(GeoPoint sourcePoint, GeoPoint destinationPoint);
}
