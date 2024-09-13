namespace DistributionCenter.Infraestructure.DTOs.Interfaces;

public interface IUpdateDto<T> : IValidatable
{
    T FromEntity(T entity);
}
