namespace DistributionCenter.Application.Tables.Core.Concretes;

using DistributionCenter.Application.Tables.Components.Information.Concretes;
using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using DistributionCenter.Application.Tables.Connections.Interfaces;
using DistributionCenter.Application.Tables.Core.Bases;
using DistributionCenter.Domain.Entities.Concretes;

public class ClientTable(IDbConnectionFactory dbConnectionFactory) : BaseDapperTable<Client>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new ClientTableInformation();
    }
}
