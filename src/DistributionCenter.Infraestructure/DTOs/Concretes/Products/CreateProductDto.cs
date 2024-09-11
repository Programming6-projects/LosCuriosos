namespace DistributionCenter.Infraestructure.DTOs.Concretes.Products;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Products;

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

    public Result Validate()
    {
        return new CreateProductValidator().Validate(this);
    }
}
