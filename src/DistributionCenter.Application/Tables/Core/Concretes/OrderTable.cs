namespace DistributionCenter.Application.Tables.Core.Concretes;

using System.Data;
using Bases;
using Components.Information.Concretes;
using Components.Information.Interfaces;
using Connections.Dapper.Interfaces;

public class OrderTable(IDbConnectionFactory<IDbConnection> dbConnectionFactory) : BaseDapperTable<Order>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new OrderTableInformation();
    }
}
