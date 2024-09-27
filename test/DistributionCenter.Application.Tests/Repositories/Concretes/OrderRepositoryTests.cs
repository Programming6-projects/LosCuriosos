namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using Application.Contexts.Interfaces;
using Application.Repositories.Concretes;
using Commons.Results;
using Domain.Entities.Concretes;
using Domain.Entities.Enums;

public class OrderRepositoryTests
{
    private readonly Mock<IContext> _contextMock;
    private readonly OrderRepository _orderRepository;

    public OrderRepositoryTests()
    {
        _contextMock = new Mock<IContext>();
        _orderRepository = new OrderRepository(_contextMock.Object);
    }

    [Fact]
    public void Constructor_InitializesRepository()
    {
        // Define Input and Output
        Mock<IContext> contextMock = new();
        OrderRepository repository = new(contextMock.Object);

        // Verify actual result
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task CreateAsync_OrderCreationSuccess_ReturnsOrder()
    {
        Order order = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            DeliveryTime = DateTime.Now,
            Status = Status.Pending
        };
        Result createResult = Result.Ok();
        _ = _contextMock.Setup(c => c.SetTable<Order>().Create(order).ExecuteAsync()).ReturnsAsync(createResult);

        Result<Order> result = await _orderRepository.CreateAsync(order);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(order, result.Value);
    }

    [Fact]
    public async Task UpdateAsync_OrderUpdateSuccess_ReturnsOrder()
    {
        Order order = new()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid(),
            DeliveryTime = DateTime.Now,
            Status = Status.Pending
        };

        Result updateResult = Result.Ok();

        _ = _contextMock.Setup(c => c.SetTable<Order>().Update(order).ExecuteAsync()).ReturnsAsync(updateResult);

        Result<Order> result = await _orderRepository.UpdateAsync(order);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(order, result.Value);
    }

    [Fact]
    public async Task GetByIdAsync_OrderWithProducts_ReturnsOrderAndProducts()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Order order = new()
        {
            Id = orderId, ClientId = Guid.NewGuid(), DeliveryPointId = Guid.NewGuid(), DeliveryTime = DateTime.Now,
            Status = Status.Pending
        };
        List<OrderProduct> orderProducts = new() { new OrderProduct
            {
                OrderId = orderId,
                ProductId = Guid.NewGuid(),
                Quantity = 0
            }
        };

        _ = _contextMock.Setup(c => c.SetTable<Order>().GetById(orderId).ExecuteAsync())
            .ReturnsAsync(new Result<Order>(order));
        _ = _contextMock.Setup(c => c.SetTable<OrderProduct>().GetAll().ExecuteAsync())
            .ReturnsAsync(new Result<IEnumerable<OrderProduct>>(orderProducts));

        foreach (OrderProduct orderProduct in orderProducts)
        {
            _ = _contextMock.Setup(c => c.SetTable<Product>().GetById(orderProduct.ProductId).ExecuteAsync())
                .ReturnsAsync(new Result<Product>(new Product
                {
                    Id = orderProduct.ProductId,
                    Name = "dddddddd",
                    Description = "ddddddd",
                    Weight = 0
                }));
        }

        // Act
        Result<Order> result = await _orderRepository.GetByIdAsync(orderId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(orderId, result.Value.Id);
        Assert.NotEmpty(result.Value.Products);
    }
}
