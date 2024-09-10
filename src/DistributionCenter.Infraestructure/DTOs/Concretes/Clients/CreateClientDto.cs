namespace DistributionCenter.Infraestructure.DTOs.Concretes.Clients;

using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;

public class CreateClientDto : ICreateDto<Client>
{
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }

    public Client ToEntity()
    {
        return new Client
        {
            Name = Name,
            LastName = LastName,
            Email = Email,
        };
    }
}
