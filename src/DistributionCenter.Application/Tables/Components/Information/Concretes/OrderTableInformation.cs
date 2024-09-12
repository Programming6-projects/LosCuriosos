namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class OrderTableInformation : BaseEntityTableInformation
{
    public OrderTableInformation()
        : base() { }

    protected override string ObtainGetByIdFields()
    {
        return "client_id AS ClientId, order_status_id AS OrderStatusId";
    }

    protected override string ObtainTableName()
    {
        return "client_order";
    }

    protected override string ObtainCreateFields()
    {
        return "client_id, order_status_id";
    }

    protected override string ObtainCreateValues()
    {
        return "@ClientId, @OrderStatusId";
    }

    protected override string ObtainUpdateFields()
    {
        return "client_id = @ClientId, order_status_id = @OrderStatusId";
    }
}
