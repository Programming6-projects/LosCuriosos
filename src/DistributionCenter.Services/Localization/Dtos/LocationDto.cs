namespace DistributionCenter.Services.Localization.Dtos;

public class LocationDto (string category, bool isInCountry, double distanceFromStore)
{
    private string _category = category;
    private bool _isInCountry = isInCountry;
    private double _distanceFromStore = distanceFromStore;

    public string Category
    {
        get => _category;
        set => _category = value;
    }

    public bool IsInCountry
    {
        get => _isInCountry;
        set => _isInCountry = value;
    }

    public double DistanceFromStore
    {
        get => _distanceFromStore;
        set => _distanceFromStore = value;
    }
}
