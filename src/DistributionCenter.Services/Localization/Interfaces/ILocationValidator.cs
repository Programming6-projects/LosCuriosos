namespace DistributionCenter.Services.Localization.Interfaces;

using System.Threading.Tasks;

public interface ILocationValidator
{
    Task<bool> IsLocationInCountryAsync(double latitude, double longitude);
}
