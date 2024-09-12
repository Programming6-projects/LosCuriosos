namespace DistributionCenter.Application.Tables.Core.Concretes;

using Bases;
using Components.Information.Concretes;
using Components.Information.Interfaces;
using Connections.File.Interfaces;
using DistributionCenter.Domain.Entities.Concretes;

public class TransportTable(IFileConnectionFactory<Transport> fileConnectionFactory) :
    BaseFileTable<Transport>(fileConnectionFactory)
{
    public override ITableInformation GetInformation()
    {
        return new TransportTableInformation();
    }
}
