namespace DistributionCenter.Application.Tables.Components.Information.Bases;

using DistributionCenter.Application.Tables.Components.Information.Interfaces;

public abstract class BaseEntityTableInformation : ITableInformation
{
    public string TableName => ObtainTableName();

    public string GetByIdFields =>
        $"id AS Id, {ObtainGetByIdFields()}, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";

    public string CreateFields => $"id, {ObtainCreateFields()}, is_active, created_at, updated_at";

    public string CreateValues => $"@Id, {ObtainCreateValues()}, @IsActive, @CreatedAt, @UpdatedAt";

    public string UpdateFields => $"{ObtainUpdateFields()}, is_active = @IsActive, updated_at = @UpdatedAt";

    protected abstract string ObtainTableName();

    protected abstract string ObtainGetByIdFields();

    protected abstract string ObtainCreateFields();

    protected abstract string ObtainCreateValues();

    protected abstract string ObtainUpdateFields();
}
