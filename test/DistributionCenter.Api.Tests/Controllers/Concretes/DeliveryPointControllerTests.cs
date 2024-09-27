namespace DistributionCenter.Api.Tests.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Domain.Entities.Concretes;

public class DeliveryPointControllerTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WithValidRepository()
    {
        Mock<IRepository<DeliveryPoint>> mockRepository = new();

        DeliveryPointController controller = new(mockRepository.Object);
        // Assert
        Assert.NotNull(controller);
    }
}
