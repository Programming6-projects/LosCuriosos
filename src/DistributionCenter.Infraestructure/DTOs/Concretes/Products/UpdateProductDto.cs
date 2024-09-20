namespace DistributionCenter.Infraestructure.DTOs.Concretes.Products;

using Commons.Results;
using Domain.Entities.Concretes;
using Interfaces;
using Validators.Core.Concretes.Products;

public class UpdateProductDto : IUpdateDto<Product>
{
    public string? Name { get; init; }
    public string? Description { get; init; }

    public Product FromEntity(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Name = Name ?? entity.Name;
        entity.Description = Description ?? entity.Description;

        return entity;
    }

    public Result Validate()
    {
        return new UpdateProductValidator().Validate(this);
    }
}
