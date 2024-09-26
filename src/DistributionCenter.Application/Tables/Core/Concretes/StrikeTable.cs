namespace DistributionCenter.Application.Tables.Core.Concretes;

using System.Data;
using Bases;
using Components.Information.Concretes;
using Components.Information.Interfaces;
using Connections.Dapper.Interfaces;

public class StrikeTable(IDbConnectionFactory<IDbConnection> dbConnectionFactory)
    : BaseDapperTable<Strike>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new StrikeTableInformation();
    }
}
