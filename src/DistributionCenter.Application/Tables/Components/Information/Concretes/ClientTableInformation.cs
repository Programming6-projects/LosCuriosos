namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class ClientTableInformation : BaseEntityTableInformation
{
    protected override string ObtainGetByIdFields()
    {
        return "name, last_name AS LastName, email";
    }

    protected override string ObtainTableName()
    {
        return "client";
    }

    protected override string ObtainCreateFields()
    {
        return "name, last_name, email";
    }

    protected override string ObtainCreateValues()
    {
        return "@Name, @LastName, @Email";
    }

    protected override string ObtainUpdateFields()
    {
        return "name = @Name, last_name = @LastName, email = @Email";
    }
}
