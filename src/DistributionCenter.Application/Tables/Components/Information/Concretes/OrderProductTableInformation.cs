namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class OrderProductTableInformation: BaseEntityTableInformation
{
    public OrderProductTableInformation()
        : base() { }

    protected override string ObtainGetByIdFields()
    {
        return "quantity AS Quantity, product_id AS ProductId, client_order_id AS OrderId";
    }

    protected override string ObtainTableName()
    {
        return "client_order_product";
    }

    protected override string ObtainCreateFields()
    {
        return "quantity, product_id, client_order_id";
    }

    protected override string ObtainCreateValues()
    {
        return "@Quantity, @ProductId, @OrderId";
    }

    protected override string ObtainUpdateFields()
    {
        return "quantity = @Quantity, product_id = @ProductId, client_order_id = @OrderId";
    }
}
