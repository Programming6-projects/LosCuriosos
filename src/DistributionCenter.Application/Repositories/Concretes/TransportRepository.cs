namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Domain.Entities.Concretes;

public class TransportRepository(IContext context) : BaseRepository<Transport>(context) { }
