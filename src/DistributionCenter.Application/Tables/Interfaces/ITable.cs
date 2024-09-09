namespace DistributionCenter.Application.Tables.Interfaces;

using DistributionCenter.Application.QueryCommands.Interfaces;
using DistributionCenter.Domain.Entities.Interfaces;

public interface ITable<T>
    where T : IEntity
{
    IQuery<T> GetByIdQuery(Guid id);
    ICommand CreateCommand(T entity);
    ICommand UpdateCommand(T entity);
}
