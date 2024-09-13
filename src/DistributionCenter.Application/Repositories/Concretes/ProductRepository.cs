namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Domain.Entities.Concretes;

public class ProductRepository(IContext context) : BaseRepository<Product>(context) { }
