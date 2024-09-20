namespace DistributionCenter.Services.Distribution.Concretes.Components.TransportsParser.Interfaces;

using Distribution.Enums;
using Domain.Entities.Concretes;

public interface ITransportParser
{
    IEnumerable<(Trip, Transport)> Parse(ICollection<Transport> transports, Location location);
}
