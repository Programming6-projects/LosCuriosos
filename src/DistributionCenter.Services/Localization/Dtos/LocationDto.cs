namespace DistributionCenter.Services.Localization.Dtos;

public class LocationDto(string category, double distanceFromStore)
{
    public string Category { get; set; } = category;
    public double DistanceFromStore { get; set; } = distanceFromStore;
}
