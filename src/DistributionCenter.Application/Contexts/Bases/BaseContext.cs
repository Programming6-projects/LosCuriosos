namespace DistributionCenter.Application.Contexts.Bases;

using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Tables.Core.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;

public abstract class BaseContext(IDictionary<Type, object> tables) : IContext
{
    protected IDictionary<Type, object> Tables { get; } = tables;

    public virtual ITable<T> SetTable<T>()
        where T : IEntity
    {
        return (ITable<T>)Tables[typeof(T)];
    }
}
