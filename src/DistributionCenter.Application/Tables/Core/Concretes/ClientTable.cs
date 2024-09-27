namespace DistributionCenter.Application.Tables.Core.Concretes;

using System.Data;
using DistributionCenter.Application.Tables.Components.Information.Concretes;
using DistributionCenter.Application.Tables.Components.Information.Interfaces;
using Bases;
using Connections.Dapper.Interfaces;

public class ClientTable(IDbConnectionFactory<IDbConnection> dbConnectionFactory) : BaseDapperTable<Client>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new ClientTableInformation();
    }
}
