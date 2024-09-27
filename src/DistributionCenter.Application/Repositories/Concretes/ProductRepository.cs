namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;

public class ProductRepository(IContext context) : BaseRepository<Product>(context) { }
