namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class TransportTableInformation : BaseEntityTableInformation
{
    protected override string ObtainGetByIdFields()
    {
        return "name, capacity, availableUnits";
    }

    protected override string ObtainTableName()
    {
        return "transport";
    }

    protected override string ObtainCreateFields()
    {
        return "name, capacity, availableUnits";
    }

    protected override string ObtainCreateValues()
    {
        return "@Name, @Capacity, @AvailableUnits";
    }

    protected override string ObtainUpdateFields()
    {
        return "name = @Name, capacity = @Capacity, availableUnits = @AvailableUnits";
    }
}
