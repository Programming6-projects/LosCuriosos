namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Domain.Entities.Concretes;

public class ClientControllerTests
{
    private readonly Mock<IRepository<Client>> _repositoryMock;

    public ClientControllerTests()
    {
        _repositoryMock = new Mock<IRepository<Client>>();
    }

    [Fact]
    public void Constructor_InitializesController()
    {
        // Define Input and Output
        ClientController controller = new(_repositoryMock.Object);

        // Verify actual result
        Assert.NotNull(controller);
    }
}
