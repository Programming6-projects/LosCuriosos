namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;

public class TripRepository(IContext context) : BaseRepository<Trip>(context)
{

}
