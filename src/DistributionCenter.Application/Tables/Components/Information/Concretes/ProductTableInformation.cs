namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class ProductTableInformation : BaseEntityTableInformation
{
    protected override string ObtainTableName()
    {
        return "product";
    }

    protected override string ObtainGetByIdFields()
    {
        return "name, description, weight_gr AS Weight";
    }

    protected override string ObtainCreateFields()
    {
        return "name, description, weight_gr";
    }

    protected override string ObtainCreateValues()
    {
        return "@Name, @Description, @Weight";
    }

    protected override string ObtainUpdateFields()
    {
        return "name = @Name, description = @Description, weight_gr = @Weight";
    }
}
