namespace DistributionCenter.Services.Distribution.Concretes;

using Components.OrdersParser.Interfaces;
using Components.TransportsParser.Interfaces;
using DistributionCenter.Domain.Entities.Enums;
using Domain.Entities.Concretes;
using Enums;
using Interfaces;

public class GreedyDistribution(IOrderParser orderParser, ITransportParser transportParser) : IDistributionStrategy
{
    private static bool FillOrder((Order, int) order, List<(Trip, Transport)> parsedTrips)
    {
        for (int j = 0; j < parsedTrips.Count; j++)
        {
            (Trip, Transport) trip = parsedTrips[j];

            if (trip.Item2.CurrentCapacity < order.Item2)
            {
                continue;
            }

            trip.Item2.CurrentCapacity -= order.Item2;
            trip.Item1.Orders.Add(order.Item1);

            return true;
        }

        return false;
    }

    public (
        IEnumerable<Trip> Trips,
        IEnumerable<Transport> UpdatedTransports,
        IEnumerable<Order> CancelledOrders
    ) DistributeOrders(ICollection<Order> orders, ICollection<Transport> transports, Location location)
    {
        List<(Order, int)> parsedOrders = orderParser.Parse(orders).ToList();
        List<(Trip, Transport)> parsedTrips = transportParser.Parse(transports, location).ToList();
        List<Order> cancelledOrders = [];

        for (int i = 0; i < parsedOrders.Count; i++)
        {
            (Order, int) order = parsedOrders[i];

            if (!FillOrder(order, parsedTrips))
            {
                order.Item1.Status = Status.Cancelled;

                cancelledOrders.Add(order.Item1);
            }
        }

        return (
            Trips: parsedTrips.Select(trip => trip.Item1),
            UpdatedTransports: parsedTrips.Select(trip => trip.Item2),
            CancelledOrders: cancelledOrders
        );
    }
}
