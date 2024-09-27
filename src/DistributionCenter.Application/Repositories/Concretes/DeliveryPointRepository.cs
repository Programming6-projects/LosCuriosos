namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;

public class DeliveryPointRepository (IContext context) : BaseRepository<DeliveryPoint>(context) { }
