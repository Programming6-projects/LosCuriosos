namespace DistributionCenter.Application.Tests.Repositories.Concretes;

using Application.Contexts.Interfaces;
using Application.Repositories.Concretes;
using Commons.Results;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Services.Localization.Commons;
using Domain.Entities.Concretes;
using Services.Localization.Interfaces;

public class DeliveryPointRepositoryTests
{
    private readonly Mock<IContext> _contextMock;
    private readonly Mock<ILocationValidator> _validatorMock;
    private readonly DeliveryPointRepository _deliveryPointRepository;

    public DeliveryPointRepositoryTests()
    {
        _contextMock = new Mock<IContext>();
        _validatorMock = new Mock<ILocationValidator>();
        _deliveryPointRepository = new DeliveryPointRepository(_contextMock.Object, _validatorMock.Object);
    }

    [Fact]
    public void Constructor_InitializesRepository()
    {
        // Define Input and Output
        Mock<IContext> contextMock = new();
        Mock<ILocationValidator> validatorMock = new();
        DeliveryPointRepository repository = new(contextMock.Object, validatorMock.Object);

        // Verify actual result
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task CreateAsync_ValidLocation_ReturnsDeliveryPoint()
    {
        DeliveryPoint deliveryPoint = new() { Latitude = 10.0, Longitude = 20.0 };
        Result locationResult = Result.Ok();
        Result<DeliveryPoint> createResult = deliveryPoint;

        _ = _validatorMock.Setup(v => v.IsLocationInCountryAsync(It.IsAny<GeoPoint>())).ReturnsAsync(locationResult);
        _ = _contextMock
            .Setup(c => c.SetTable<DeliveryPoint>().Create(deliveryPoint).ExecuteAsync())
            .ReturnsAsync(createResult);

        Result<DeliveryPoint> result = await _deliveryPointRepository.CreateAsync(deliveryPoint);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(deliveryPoint, result.Value);
    }

    [Fact]
    public async Task CreateAsync_InvalidLocation_ReturnsError()
    {
        DeliveryPoint deliveryPoint = new() { Latitude = 10.0, Longitude = 20.0 };
        Result locationResult = Error.Unexpected("Invalid location");

        _ = _validatorMock
            .Setup(static v => v.IsLocationInCountryAsync(It.IsAny<GeoPoint>()))
            .ReturnsAsync(locationResult);

        Result<DeliveryPoint> result = await _deliveryPointRepository.CreateAsync(deliveryPoint);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(locationResult.Errors, result.Errors);
    }

    [Fact]
    public async Task UpdateAsync_ValidLocation_ReturnsDeliveryPoint()
    {
        DeliveryPoint deliveryPoint = new() { Latitude = 10.0, Longitude = 20.0 };
        Result locationResult = Result.Ok();
        Result<DeliveryPoint> updateResult = deliveryPoint;

        _ = _validatorMock.Setup(v => v.IsLocationInCountryAsync(It.IsAny<GeoPoint>())).ReturnsAsync(locationResult);
        _ = _contextMock
            .Setup(c => c.SetTable<DeliveryPoint>().Update(deliveryPoint).ExecuteAsync())
            .ReturnsAsync(updateResult);

        Result<DeliveryPoint> result = await _deliveryPointRepository.UpdateAsync(deliveryPoint);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(deliveryPoint, result.Value);
    }

    [Fact]
    public async Task UpdateAsync_InvalidLocation_ReturnsError()
    {
        DeliveryPoint deliveryPoint = new() { Latitude = 10.0, Longitude = 20.0 };
        Result locationResult = Error.Unexpected("Invalid location");

        _ = _validatorMock
            .Setup(static v => v.IsLocationInCountryAsync(It.IsAny<GeoPoint>()))
            .ReturnsAsync(locationResult);

        Result<DeliveryPoint> result = await _deliveryPointRepository.UpdateAsync(deliveryPoint);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(locationResult.Errors, result.Errors);
    }
}
