namespace DistributionCenter.Application.Tables.Core.Concretes;

using Bases;
using Components.Information.Concretes;
using Components.Information.Interfaces;
using Connections.Interfaces;
using Domain.Entities.Concretes;

public class ProductTable(IDbConnectionFactory dbConnectionFactory) : BaseDapperTable<Product>(dbConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new ProductTableInformation();
    }
}
