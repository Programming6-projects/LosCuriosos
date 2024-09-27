namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using Application.Contexts.Interfaces;
using Application.Repositories.Concretes;
using Commons.Results;
using Domain.Entities.Concretes;

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
        Order order = new ()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid()
        };
        Result createResult = Result.Ok();
        _ = _contextMock.Setup(c => c.SetTable<Order>().Create(order).ExecuteAsync())
            .ReturnsAsync(createResult);

        Result<Order> result = await _orderRepository.CreateAsync(order);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(order, result.Value);
    }

    [Fact]
    public async Task UpdateAsync_OrderUpdateSuccess_ReturnsOrder()
    {
        Order order = new ()
        {
            ClientId = Guid.NewGuid(),
            DeliveryPointId = Guid.NewGuid()
        };

        Result updateResult = Result.Ok();

        _ = _contextMock.Setup(c => c.SetTable<Order>().Update(order).ExecuteAsync())
            .ReturnsAsync(updateResult);

        Result<Order> result = await _orderRepository.UpdateAsync(order);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(order, result.Value);
    }
}
