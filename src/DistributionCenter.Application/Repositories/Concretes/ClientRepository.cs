namespace DistributionCenter.Application.Repositories.Concretes;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Repositories.Bases;
using DistributionCenter.Domain.Entities.Concretes;

public class ClientRepository(IContext context) : BaseRepository<Client>(context) { }
