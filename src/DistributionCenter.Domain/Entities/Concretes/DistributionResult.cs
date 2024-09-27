namespace DistributionCenter.Domain.Entities.Concretes;

public class DistributionResult
{
    public int OrdersAssigned { get; set; }
    public IEnumerable<Trip>? Trips { get; set; }
    public int TripsCount { get; set; }
    public IEnumerable<Transport>? UpdatedTransports { get; set; }
    public int UpdatedTransportsCount { get; set; }
    public IEnumerable<Order>? CancelledOrders { get; set; }
    public int CancelledOrdersCount { get; set; }
}
