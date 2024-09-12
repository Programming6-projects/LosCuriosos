namespace DistributionCenter.Application.Tables.Core.Concretes;

using System.Data;
using Bases;
using Components.Information.Concretes;
using Components.Information.Interfaces;
using Connections.Dapper.Interfaces;
using Domain.Entities.Concretes;

public class ProductTable(IDbConnectionFactory<IDbConnection> dbConnectionFactory) : BaseDapperTable<Product>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new ProductTableInformation();
    }
}
