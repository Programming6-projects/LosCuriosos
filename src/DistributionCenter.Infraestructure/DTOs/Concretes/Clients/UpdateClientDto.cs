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

    public Client FromEntity(Client entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Name = Name ?? entity.Name;
        entity.LastName = LastName ?? entity.LastName;
        entity.Email = Email ?? entity.Email;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateClientValidator().Validate(this);
    }
}
