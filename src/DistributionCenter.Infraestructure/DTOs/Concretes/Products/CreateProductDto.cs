namespace DistributionCenter.Infraestructure.DTOs.Concretes.Products;

using Domain.Entities.Concretes;
using Interfaces;

public class CreateProductDto : ICreateDto<Product>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required double Weight { get; init; }

    public Product ToEntity()
    {
        return new Product
        {
            Name = Name,
            Description = Description,
            Weight = Weight
        };
    }
}
