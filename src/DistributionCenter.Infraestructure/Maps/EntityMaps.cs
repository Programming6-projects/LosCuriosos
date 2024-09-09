namespace DistributionCenter.Infraestructure.Maps;

using AutoMapper;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Clients;

public class EntityMaps : Profile
{
    public EntityMaps()
    {
        _ = CreateMap<CreateClientDto, Client>();
        _ = CreateMap<Client, CreateClientDto>();

        CreateMap<UpdateClientDto, Client>()
            .ForAllMembers(static opt => opt.Condition(static (src, dest, srcMember) => srcMember != null));
    }
}
