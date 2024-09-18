namespace DistributionCenter.Services.Localization.Interfaces;

using System.Threading.Tasks;
using Commons;
using DistributionCenter.Commons.Results;

public interface ILocationValidator
{
    Task<Result> IsLocationInCountryAsync(GeoPoint point);
}
