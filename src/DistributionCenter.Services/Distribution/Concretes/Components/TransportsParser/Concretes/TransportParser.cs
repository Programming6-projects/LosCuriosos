namespace DistributionCenter.Services.Distribution.Concretes.Components.TransportsParser.Concretes;

using Distribution.Enums;
using Domain.Entities.Concretes;
using Interfaces;

public class TransportParser : ITransportParser
{
    public IEnumerable<(Trip, Transport)> Parse(ICollection<Transport> transports, Location location)
    {
        IEnumerable<Transport> parsedTransports = transports.Where(static t => t.IsAvailable);

        parsedTransports =
            location == Location.InCity
                ? parsedTransports.Where(static t => t.Capacity < 70000)
                : parsedTransports.Where(static t => t.Capacity >= 70000);

        IEnumerable<(Trip, Transport)> parsedRoutes = parsedTransports.Select(static t =>
            (new Trip() { TransportId = t.Id }, t)
        );

        parsedRoutes = parsedRoutes.OrderByDescending(static r => r.Item2.CurrentCapacity);

        return parsedRoutes;
    }
}
