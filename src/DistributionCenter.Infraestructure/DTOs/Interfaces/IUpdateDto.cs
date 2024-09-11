namespace DistributionCenter.Infraestructure.DTOs.Interfaces;

public interface IUpdateDto<T>
{
    T FromEntity(T entity);
}
