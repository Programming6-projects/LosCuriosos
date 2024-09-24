namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Domain.Entities.Concretes;

public class OrderProductRepository(IContext context) : BaseRepository<OrderProduct>(context) { }
