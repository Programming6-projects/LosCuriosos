namespace DistributionCenter.Application.Tables.Components.Information.Interfaces;

public interface ITableInformation
{
    string TableName { get; }

    string GetByIdFields { get; }

    string CreateFields { get; }

    string CreateValues { get; }

    string UpdateFields { get; }
}
