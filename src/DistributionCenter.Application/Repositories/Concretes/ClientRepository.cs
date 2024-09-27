namespace DistributionCenter.Application.Repositories.Concretes;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Repositories.Bases;

public class ClientRepository(IContext context) : BaseRepository<Client>(context) { }
