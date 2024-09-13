namespace DistributionCenter.Infraestructure.DTOs.Interfaces;

public interface ICreateDto<T> : IValidatable
{
    T ToEntity();
}
