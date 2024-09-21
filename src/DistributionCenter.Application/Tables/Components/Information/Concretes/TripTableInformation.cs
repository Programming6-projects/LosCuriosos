namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class TripTableInformation : BaseEntityTableInformation
{
    protected override string ObtainTableName()
    {
        return "route";
    }

    protected override string ObtainGetByIdFields()
    {
        return "status, transport_id AS TransportId";
    }

    protected override string ObtainCreateFields()
    {
        return "status, transport_id";
    }

    protected override string ObtainCreateValues()
    {
        return "@Status, @TransportId";
    }

    protected override string ObtainUpdateFields()
    {
        return "status = @Status, transport_id = @TransportId";
    }
}
