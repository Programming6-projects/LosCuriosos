namespace DistributionCenter.Services.Distribution.Interfaces;

using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Services.Distribution.Enums;

public interface IDistributionStrategy
{
    public (
        IEnumerable<Trip> Trips,
        IEnumerable<Transport> UpdatedTransports,
        IEnumerable<Order> CancelledOrders
    ) DistributeOrders(ICollection<Order> orders, ICollection<Transport> transports, Location location);
}
