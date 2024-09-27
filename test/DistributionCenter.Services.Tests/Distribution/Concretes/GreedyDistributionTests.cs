namespace DistributionCenter.Services.Tests.Distribution.Concretes;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;

public class GreedyDistributionTests
{
    private readonly Mock<IOrderParser> _orderParserMock;
    private readonly Mock<ITransportParser> _transportParserMock;
    private readonly GreedyDistribution _distribution;

    public GreedyDistributionTests()
    {
        _orderParserMock = new Mock<IOrderParser>();
        _transportParserMock = new Mock<ITransportParser>();
        _distribution = new GreedyDistribution(_orderParserMock.Object, _transportParserMock.Object);
    }

    [Fact]
    public void DistributeOrders_DistributesOrdersCorrectly()
    {
        // Define Input and Output
        List<Order> orders =
        [
            new Order
            {
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
                ],
                Status = Status.Pending,
            },
            new Order
            {
                RouteId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                DeliveryPointId = Guid.NewGuid(),

                Products =
                [
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
                Status = Status.Pending,
            }
        ];

        List<Transport> transports =
        [
            new Transport
            {
                Name = "Transport 1",
                Plate = "Plate 1",
                Capacity = 100,
                CurrentCapacity = 100,
                IsAvailable = true,
            },
            new Transport
            {
                Name = "Transport 2",
                Plate = "Plate 2",
                Id = Guid.NewGuid(),
                Capacity = 50,
                CurrentCapacity = 50,
                IsAvailable = true,
            },
        ];

        _ = _orderParserMock
            .Setup(parser => parser.Parse(orders))
            .Returns(orders.Select(o => (o, o.Products.Sum(p => p.Quantity * p.Product.Weight))));
        _ = _transportParserMock
            .Setup(parser => parser.Parse(transports, Location.InCity))
            .Returns(transports.Select(t => (new Trip
            {
                TransportId = t.Id,
                Status = Status.Pending
            }, t)));

        // Execute Actual Operation
        (IEnumerable<Trip> Trips, IEnumerable<Transport> UpdatedTransports, IEnumerable<Order> CancelledOrders) =
            _distribution.DistributeOrders(orders, transports, Location.InCity);

        // Verify Actual Result
        _ = Assert.Single(Trips);
        _ = Assert.Single(UpdatedTransports);
        Assert.Empty(CancelledOrders);

        _orderParserMock.Verify(parser => parser.Parse(orders), Times.Once);
        _transportParserMock.Verify(parser => parser.Parse(transports, Location.InCity), Times.Once);
    }
}
