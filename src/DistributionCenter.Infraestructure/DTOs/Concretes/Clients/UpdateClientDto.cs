namespace DistributionCenter.Infraestructure.DTOs.Concretes.Clients;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Interfaces;
using DistributionCenter.Infraestructure.Validators.Core.Concretes.Clients;

public class UpdateClientDto : IUpdateDto<Client>
{
    public string? Name { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }

    public Client FromEntity(Client client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        client.Name = Name ?? client.Name;
        client.LastName = LastName ?? client.LastName;
        client.Email = Email ?? client.Email;

        return client;
    }

    public Result Validate()
    {
        return new UpdateClientValidator().Validate(this);
    }
}
