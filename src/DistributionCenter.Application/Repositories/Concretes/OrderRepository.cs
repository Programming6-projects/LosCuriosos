namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;

public class OrderRepository(IContext context) : BaseRepository<Order>(context) { }
