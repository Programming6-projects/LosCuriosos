namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Contexts.Interfaces;
using Commons.Results;
using Domain.Entities.Concretes;

public class OrderRepository(IContext context) : BaseRepository<Order>(context)
{
    public override async Task<Result<Order>> GetByIdAsync(Guid id)
    {
        Result<Order> orderResult = await base.GetByIdAsync(id);

        if (!orderResult.IsSuccess)
        {
            return orderResult.Errors;
        }

        Result<IEnumerable<OrderProduct>> orderProductsResult = await Context.SetTable<OrderProduct>().GetAll().ExecuteAsync();

        if (!orderProductsResult.IsSuccess)
        {
            return orderProductsResult.Errors;
        }

        Order order = orderResult.Value;

        IEnumerable<OrderProduct> productsOrder = orderProductsResult.Value.Where(p => p.OrderId == order.Id);

        foreach (OrderProduct orderProduct in productsOrder)
        {
            Result<Product> product = await Context.SetTable<Product>().GetById(orderProduct.ProductId).ExecuteAsync();

            if (!product.IsSuccess)
            {
                return product.Errors;
            }

            orderProduct.Product = product.Value;
        }

        order.Products = productsOrder;

        return order;
    }


    public override async Task<Result<Order>> CreateAsync(Order entity)
    {
        IEnumerable<OrderProduct> products = entity.Products;

        foreach (OrderProduct product in products)
        {
            Console.WriteLine(product.Id);
            Console.WriteLine(product.OrderId);
            Console.WriteLine(product.ProductId);
            Console.WriteLine(product.Quantity);

            Result resultInsert = await Context.SetTable<OrderProduct>().Create(product).ExecuteAsync();

            if (!resultInsert.IsSuccess)
            {
                return resultInsert.Errors;
            }
        }

        return await base.CreateAsync(entity);
    }

    public override async Task<Result<Order>> UpdateAsync(Order entity)
    {
        IEnumerable<OrderProduct> products = entity.Products;

        foreach (OrderProduct product in products)
        {
            Result resultInsert = await Context.SetTable<OrderProduct>().Update(product).ExecuteAsync();

            if (!resultInsert.IsSuccess)
            {
                return resultInsert.Errors;
            }
        }

        return await base.UpdateAsync(entity);
    }
}
