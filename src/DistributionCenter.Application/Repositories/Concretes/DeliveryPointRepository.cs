namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Domain.Entities.Concretes;

public class DeliveryPointRepository(IContext context) : BaseRepository<DeliveryPoint>(context) { }
