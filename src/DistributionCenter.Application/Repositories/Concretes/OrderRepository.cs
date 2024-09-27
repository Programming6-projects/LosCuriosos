namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Commons.Results;
using Contexts.Interfaces;

public class OrderRepository(IContext context) : BaseRepository<Order>(context)
{
    private async Task<Result<IEnumerable<OrderProduct>>> GetProductsByOrderId(Guid id)
    {
        Result<IEnumerable<OrderProduct>> orderProductsResult = await Context
            .SetTable<OrderProduct>()
            .GetAll()
            .ExecuteAsync();

        if (!orderProductsResult.IsSuccess)
        {
            return orderProductsResult.Errors;
        }

        List<OrderProduct> productsOrder = orderProductsResult.Value.Where(p => p.OrderId == id).ToList();

        foreach (OrderProduct orderProduct in productsOrder)
        {
            Result<Product> product = await Context.SetTable<Product>().GetById(orderProduct.ProductId).ExecuteAsync();

            if (!product.IsSuccess)
            {
                return product.Errors;
            }

            orderProduct.Product = product.Value;
        }

        return productsOrder;
    }

    public override async Task<Result<Order>> GetByIdAsync(Guid id)
    {
        Result<Order> orderResult = await base.GetByIdAsync(id);

        if (!orderResult.IsSuccess)
        {
            return orderResult.Errors;
        }

        Order order = orderResult.Value;

        Result<IEnumerable<OrderProduct>> productsResult = await GetProductsByOrderId(order.Id);

        if (!productsResult.IsSuccess)
        {
            return productsResult.Errors;
        }

        order.Products = productsResult.Value;

        return order;
    }

    public override async Task<Result<Order>> CreateAsync(Order entity)
    {
        Result<Order> result = await base.CreateAsync(entity);

        if (!result.IsSuccess)
        {
            return result.Errors;
        }

        IEnumerable<OrderProduct> products = entity.Products;

        foreach (OrderProduct product in products)
        {
            Result resultInsert = await Context.SetTable<OrderProduct>().Create(product).ExecuteAsync();

            if (!resultInsert.IsSuccess)
            {
                return resultInsert.Errors;
            }

            Result<Product> productResult = await Context.SetTable<Product>().GetById(product.ProductId).ExecuteAsync();

            if (!productResult.IsSuccess)
            {
                return productResult.Errors;
            }

            product.Product = productResult.Value;
        }

        return entity;
    }

    public override async Task<Result<Order>> UpdateAsync(Order entity)
    {
        Result<Order> result = await base.UpdateAsync(entity);

        if (!result.IsSuccess)
        {
            return result.Errors;
        }

        IEnumerable<OrderProduct> products = entity.Products;

        foreach (OrderProduct product in products)
        {
            Result resultInsert = await Context.SetTable<OrderProduct>().Update(product).ExecuteAsync();

            if (!resultInsert.IsSuccess)
            {
                return resultInsert.Errors;
            }

            Result<Product> productResult = await Context.SetTable<Product>().GetById(product.ProductId).ExecuteAsync();

            if (!productResult.IsSuccess)
            {
                return productResult.Errors;
            }

            product.Product = productResult.Value;
        }

        return entity;
    }
}
