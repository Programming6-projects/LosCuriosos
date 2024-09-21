namespace DistributionCenter.Services.Tests.Distribution.Concretes.Components.OrdersParser.Concretes;

using DistributionCenter.Services.Distribution.Concretes.Components.OrdersParser.Concretes;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;

public class OrderParserTests
{
    [Fact]
    public void Parse_ReturnsParsedOrders_WhenOrdersArePending()
    {
        // Define Input and Output
        List<Order> orders =
        [
            new Order
            {
                Status = Status.Pending,
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Products =
                [
                    new OrderProduct
                    {
                        ProductId = Guid.NewGuid(),
                        OrderId = Guid.NewGuid(),
                        Quantity = 2,
                        Product = new Product
                        {
                            Name = "Product 1",
                            Description = "Description 1",
                            Weight = 5,
                        },
                    },
                    new OrderProduct
                    {
                        ProductId = Guid.NewGuid(),
                        OrderId = Guid.NewGuid(),
                        Quantity = 1,
                        Product = new Product
                        {
                            Name = "Product 2",
                            Description = "Description 2",
                            Weight = 10,
                        },
                    },
                ],
            },
            new Order
            {
                Status = Status.Pending,
                ClientId = Guid.NewGuid(),
                RouteId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),
                Products =
                [
                    new OrderProduct
                    {
                        ProductId = Guid.NewGuid(),
                        OrderId = Guid.NewGuid(),
                        Quantity = 3,
                        Product = new Product
                        {
                            Name = "Product 3",
                            Description = "Description 3",
                            Weight = 2,
                        },
                    },
                ],
            },
        ];

        OrderParser parser = new();

        // Execute actual Operation
        List<(Order, int)> result = parser.Parse(orders).ToList();

        // Verify Actual Result
        Assert.Equal(2, result.Count);
        Assert.Equal(20, result[0].Item2);
        Assert.Equal(6, result[1].Item2);
    }
}
