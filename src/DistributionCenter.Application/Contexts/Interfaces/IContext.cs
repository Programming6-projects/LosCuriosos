namespace DistributionCenter.Application.Contexts.Interfaces;

using DistributionCenter.Application.Tables.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;

public interface IContext
{
    ITable<T> SetTable<T>()
        where T : IEntity;
}
