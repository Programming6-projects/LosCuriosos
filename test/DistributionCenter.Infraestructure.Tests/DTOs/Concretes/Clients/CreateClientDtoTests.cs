namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Clients;

using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Concretes.Clients;

public class CreateClientDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectClient()
    {
        // Define Input and Output
        CreateClientDto dto =
            new()
            {
                Name = "Test Name",
                LastName = "Test LastName",
                Email = "test@example.com",
            };

        // Execute actual operation
        Client client = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.Name, client.Name);
        Assert.Equal(dto.LastName, client.LastName);
        Assert.Equal(dto.Email, client.Email);
    }
}
