namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Domain.Entities.Concretes;

public class OrderControllerTests
{
    private readonly Mock<IRepository<Order>> _repositoryMock;

    public OrderControllerTests()
    {
        _repositoryMock = new Mock<IRepository<Order>>();
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        OrderController controller = new(_repositoryMock.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }
}
