namespace DistributionCenter.Infraestructure.DTOs.Interfaces;

public interface ICreateDto<T>
{
    T ToEntity();
}
