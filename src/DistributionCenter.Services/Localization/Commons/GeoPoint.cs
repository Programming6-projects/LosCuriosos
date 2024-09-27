namespace DistributionCenter.Services.Localization.Commons;

public class GeoPoint(double latitude, double longitude)
{
    private const double Tolerance = 0.0000001;
    public double Latitude { get; } = latitude;
    public double Longitude { get; } = longitude;

    public override bool Equals(object? obj)
    {
        if (obj is not GeoPoint other) return false;
        return Math.Abs(Latitude - other.Latitude) < Tolerance
               && Math.Abs(Longitude - other.Longitude) < Tolerance;
    }

    public override int GetHashCode()
    {
        int latHash = (Latitude / Tolerance).GetHashCode();
        int lonHash = (Longitude / Tolerance).GetHashCode();

        unchecked
        {
            int hash = 17;
            hash = hash * 23 + latHash;
            hash = hash * 23 + lonHash;
            return hash;
        }
    }
}
