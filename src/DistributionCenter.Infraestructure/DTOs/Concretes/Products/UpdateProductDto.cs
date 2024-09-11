namespace DistributionCenter.Infraestructure.DTOs.Concretes.Products;

using Domain.Entities.Concretes;
using Interfaces;

public class UpdateProductDto : IUpdateDto<Product>
{
    public required string? Name { get; init; }
    public required string? Description { get; init; }

    public Product FromEntity(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        entity.Name = Name ?? entity.Name;
        entity.Description = Description ?? entity.Description;

        return entity;
    }
}
