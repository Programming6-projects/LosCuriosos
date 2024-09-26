namespace DistributionCenter.Application.Tables.Core.Interfaces;

using DistributionCenter.Application.Tables.Components.QueryCommands.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;

public interface ITable<T>
    where T : IEntity
{
    IQuery<T> GetById(Guid id);
    IQuery<IEnumerable<T>> GetAll();

    IMultipleResponseQuery<T> SelectWhere(Func<T, bool> predicate);
    ICommand Create(T entity);
    ICommand Update(T entity);

}
