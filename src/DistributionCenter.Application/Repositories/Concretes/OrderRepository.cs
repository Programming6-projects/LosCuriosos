namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Domain.Entities.Concretes;

public class OrderRepository(IContext context) : BaseRepository<ClientOrder>(context) { }
