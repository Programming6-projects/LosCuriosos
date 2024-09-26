namespace DistributionCenter.Application.Repositories.Concretes;

using Bases;
using Commons.Results;
using Contexts.Interfaces;
using Domain.Entities.Concretes;

public class OrderRepository(IContext context) : BaseRepository<Order>(context)
{
    private async Task<Result<Order>> GetOrderWithProducts(Order order)
    {
        Result<IEnumerable<OrderProduct>> resultOrderProducts = await Context.SetTable<OrderProduct>().GetAll().ExecuteAsync();

        if (!resultOrderProducts.IsSuccess)
        {
            return resultOrderProducts.Errors;
        }

        order.Products = resultOrderProducts.Value.Where(orderProduct => orderProduct.OrderId == order.Id).ToArray();

        return order;
    }

    public override async Task<Result<Order>> GetByIdAsync(Guid id)
    {
        Result<Order> resultTransport = await base.GetByIdAsync(id);

        if (!resultTransport.IsSuccess)
        {
            return resultTransport.Errors;
        }

        return await GetOrderWithProducts(resultTransport.Value);
    }
}
