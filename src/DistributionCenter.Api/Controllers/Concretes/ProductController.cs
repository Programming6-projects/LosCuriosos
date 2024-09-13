namespace DistributionCenter.Api.Controllers.Concretes;

using Application.Repositories.Interfaces;
using Bases;
using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.Products;
using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
public class ProductController(IRepository<Product> repository)
    : BaseEntityController<Product, CreateProductDto, UpdateProductDto>(repository)
{

}
