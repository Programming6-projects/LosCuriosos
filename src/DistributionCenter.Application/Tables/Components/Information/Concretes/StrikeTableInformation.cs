namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class StrikeTableInformation : BaseEntityTableInformation
{
    protected override string ObtainTableName()
    {
        return "strike";
    }

    protected override string ObtainGetByIdFields()
    {
        return "description, transport_id AS TransportId";
    }

    protected override string ObtainCreateFields()
    {
        return "description, transport_id";
    }

    protected override string ObtainCreateValues()
    {
        return "@Description, @TransportId";
    }

    protected override string ObtainUpdateFields()
    {
        return "description = @Description, transport_id = @TransportId";
    }
}
