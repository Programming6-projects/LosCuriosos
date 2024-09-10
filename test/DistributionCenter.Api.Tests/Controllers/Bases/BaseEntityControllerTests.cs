namespace DistributionCenter.Api.Tests.Controllers.Bases;

using DistributionCenter.Api.Controllers.Bases;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Commons.Errors;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Concretes.Clients;
using Microsoft.AspNetCore.Mvc;

public class BaseEntityControllerTests
{
    private readonly Mock<IRepository<Client>> _repositoryMock;
    private readonly Mock<BaseEntityController<Client, CreateClientDto, UpdateClientDto>> _controllerMock;

    public BaseEntityControllerTests()
    {
        _repositoryMock = new Mock<IRepository<Client>>();
        _controllerMock = new Mock<BaseEntityController<Client, CreateClientDto, UpdateClientDto>>(
            _repositoryMock.Object
        )
        {
            CallBase = true,
        };
    }

    [Fact]
    public async Task Create_ReturnsOkResult()
    {
        // Define Input and Output
        CreateClientDto dto =
            new()
            {
                Name = "Test",
                LastName = "Test",
                Email = "test@test.com",
            };
        Client client = dto.ToEntity();

        _ = _repositoryMock.Setup(static r => r.CreateAsync(It.IsAny<Client>())).ReturnsAsync(client);

        // Execute actual operation
        IActionResult result = await _controllerMock.Object.Create(dto);

        // Verify actual result
        _ = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GeyById_ReturnsOkResult()
    {
        // Define Input and Output
        Guid id = Guid.NewGuid();
        Client client =
            new()
            {
                Name = "Test",
                LastName = "Test",
                Email = "test@test.com",
            };

        _ = _repositoryMock.Setup(static r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(client);

        // Execute actual operation
        IActionResult result = await _controllerMock.Object.GetById(id);

        // Verify actual result
        _ = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_NotFound_ReturnsProblemResult()
    {
        // Define Input and Output
        Guid id = Guid.NewGuid();
        UpdateClientDto dto =
            new()
            {
                Name = "Test",
                LastName = "Test",
                Email = "test@test.com",
            };

        _ = _repositoryMock.Setup(static r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(Error.NotFound());

        // Execute actual operation
        IActionResult result = await _controllerMock.Object.Update(id, dto);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        _ = Assert.IsType<ProblemDetails>(objectResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsOkResult()
    {
        // Define Input and Output
        Guid id = Guid.NewGuid();
        UpdateClientDto dto = new();
        Client client = dto.FromEntity(
            new Client()
            {
                Name = "Test",
                LastName = "Test",
                Email = "test@test.com",
            }
        );

        _ = _repositoryMock.Setup(static r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(client);
        _ = _repositoryMock.Setup(static r => r.UpdateAsync(It.IsAny<Client>())).ReturnsAsync(client);

        // Execute actual operation
        IActionResult result = await _controllerMock.Object.Update(id, dto);

        // Verify actual result
        _ = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Disable_NotFound_ReturnsProblemResult()
    {
        // Define Input and Output
        Guid id = Guid.NewGuid();

        _ = _repositoryMock.Setup(static r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(Error.NotFound());

        // Execute actual operation
        IActionResult result = await _controllerMock.Object.Disable(id);

        // Verify actual result
        ObjectResult objectResult = Assert.IsType<ObjectResult>(result);
        _ = Assert.IsType<ProblemDetails>(objectResult.Value);
    }

    [Fact]
    public async Task Disable_ReturnsOkResult()
    {
        // Define Input and Output
        Guid id = Guid.NewGuid();
        Client client =
            new()
            {
                Name = "Test",
                LastName = "Test",
                Email = "test@test.com",
            };

        _ = _repositoryMock.Setup(static r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(client);
        _ = _repositoryMock.Setup(static r => r.UpdateAsync(It.IsAny<Client>())).ReturnsAsync(client);

        // Execute actual operation
        IActionResult result = await _controllerMock.Object.Disable(id);

        // Verify actual result
        _ = Assert.IsType<OkObjectResult>(result);
    }
}
