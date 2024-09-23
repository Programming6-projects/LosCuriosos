namespace DistributionCenter.Services.Distribution.Concretes.Components.OrdersParser.Concretes;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Interfaces;

public class OrderParser : IOrderParser
{
    public IEnumerable<(Order, int)> Parse(ICollection<Order> orders)
    {
        IEnumerable<(Order, int)> parsedOrders = orders
            .Where(static o => o.Status == Status.Pending)
            .Select(static o => (o, o.Products.Sum(static p => p.Quantity * p.Product.Weight)));

        parsedOrders = parsedOrders.OrderByDescending(static o => o.Item2);

        return parsedOrders;
    }
}
