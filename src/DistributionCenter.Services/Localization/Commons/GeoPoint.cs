namespace DistributionCenter.Services.Localization.Commons;

public class GeoPoint(double latitude, double longitude)
{
    public double Latitude { get; set; } = latitude;
    public double Longitude { get; set;  } = longitude;

    public override bool Equals(object? obj)
    {
        if (obj is not GeoPoint other) return false;
        const double tolerance = 0.000001;
        return Math.Abs(Latitude - other.Latitude) < tolerance &&
               Math.Abs(Longitude - other.Longitude) < tolerance;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Math.Round(Latitude, 4), Math.Round(Longitude, 4));
    }
}
