namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Domain.Entities.Concretes;

public class ProductControllerTests
{
    private readonly Mock<IRepository<Product>> _repositoryMock;

    public ProductControllerTests()
    {
        _repositoryMock = new Mock<IRepository<Product>>();
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        ProductController controller = new(_repositoryMock.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }
}
