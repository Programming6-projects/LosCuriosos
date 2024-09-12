namespace DistributionCenter.Application.Tables.Core.Concretes;

using Components.Information.Concretes;
using Components.Information.Interfaces;
using Connections.Interfaces;
using Bases;
using DistributionCenter.Domain.Entities.Concretes;

public class TransportTable(IFileConnectionFactory<Transport> fileConnectionFactory) :
    BaseJsonTable<Transport>(fileConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new TransportTableInformation();
    }
}
