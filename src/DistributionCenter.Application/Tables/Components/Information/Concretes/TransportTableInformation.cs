namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class TransportTableInformation : BaseEntityTableInformation
{
    protected override string ObtainGetByIdFields()
    {
        return "name, plate, capacity, currentCapacity, isAvailable";
    }

    protected override string ObtainTableName()
    {
        return "transport";
    }

    protected override string ObtainCreateFields()
    {
        return "name, plate, capacity, currentCapacity, isAvailable";
    }

    protected override string ObtainCreateValues()
    {
        return "@Name, @Plate, @Capacity, @CurrentCapacity, @IsAvailable";
    }

    protected override string ObtainUpdateFields()
    {
        return "name = @Name, plate = @Plate, capacity = @Capacity, currentCapacity = @CurrentCapacity, isAvailable = @IsAvailable";
    }
}
