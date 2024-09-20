namespace DistributionCenter.Services.Localization.Commons;

public class GeoPoint(double latitude, double longitude)
{
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set; } = longitude;
}
