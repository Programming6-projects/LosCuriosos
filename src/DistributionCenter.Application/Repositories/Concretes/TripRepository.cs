namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Domain.Entities.Concretes;

public class TripRepository(IContext context) : BaseRepository<Trip>(context)
{

}
