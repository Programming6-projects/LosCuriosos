namespace DistributionCenter.Services.Distribution.Concretes.Components.OrdersParser.Interfaces;

using Domain.Entities.Concretes;

public interface IOrderParser
{
    IEnumerable<(Order, int)> Parse(ICollection<Order> orders);
}
