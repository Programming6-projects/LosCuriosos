namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Clients;

using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Concretes.Clients;

public class UpdateClientDtoTests
{
    [Fact]
    public void FromEntity_UpdatesAndReturnsCorrectClient()
    {
        // Define Input and Output
        Client client =
            new()
            {
                Name = "Old Name",
                LastName = "Old LastName",
                Email = "old@example.com",
            };
        UpdateClientDto dto =
            new()
            {
                Name = "New Name",
                LastName = "New LastName",
                Email = "new@example.com",
            };

        // Execute actual operation
        Client updatedClient = dto.FromEntity(client);

        // Verify actual result
        Assert.Equal(dto.Name, updatedClient.Name);
        Assert.Equal(dto.LastName, updatedClient.LastName);
        Assert.Equal(dto.Email, updatedClient.Email);
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectClient()
    {
        // Define Input and Output
        Client client =
            new()
            {
                Name = "Old Name",
                LastName = "Old LastName",
                Email = "old@example.com",
            };
        UpdateClientDto dto = new();

        // Execute actual operation
        Client updatedClient = dto.FromEntity(client);

        // Verify actual result
        Assert.Equal(client.Name, updatedClient.Name);
        Assert.Equal(client.LastName, updatedClient.LastName);
        Assert.Equal(client.Email, updatedClient.Email);
    }
}
