namespace DistributionCenter.Application.Tables.Core.Concretes;

using System.Data;
using Bases;
using Components.Information.Concretes;
using Components.Information.Interfaces;
using Connections.Dapper.Interfaces;

public class TripTable(IDbConnectionFactory<IDbConnection> dbConnectionFactory) : BaseDapperTable<Domain.Entities.Concretes.Trip>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new TripTableInformation();
    }
}
