namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class DeliveryPointTableInformation : BaseEntityTableInformation
{
    public DeliveryPointTableInformation()
        : base() { }

    protected override string ObtainGetByIdFields()
    {
        return "latitude AS Latitude, longitude AS Longitude";
    }

    protected override string ObtainTableName()
    {
        return "delivery_point";
    }

    protected override string ObtainCreateFields()
    {
        return "latitude, longitude";
    }

    protected override string ObtainCreateValues()
    {
        return "@Latitude, @Longitude";
    }

    protected override string ObtainUpdateFields()
    {
        return "latitude = @Latitude, longitude = @Longitude";
    }
}
