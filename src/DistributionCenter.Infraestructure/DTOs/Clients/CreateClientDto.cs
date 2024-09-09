namespace DistributionCenter.Infraestructure.DTOs.Clients;

public class CreateClientDto
{
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
}
