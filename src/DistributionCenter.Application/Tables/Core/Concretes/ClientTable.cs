namespace DistributionCenter.Application.Tables.Core.Concretes;

using System.Data;
using Connections.Interfaces;
using DistributionCenter.Application.Tables.Components.Information.Concretes;
using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using Bases;
using DistributionCenter.Domain.Entities.Concretes;

public class ClientTable(IDbConnectionFactory<IDbConnection> dbConnectionFactory) : BaseDapperTable<Client>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new ClientTableInformation();
    }
}
