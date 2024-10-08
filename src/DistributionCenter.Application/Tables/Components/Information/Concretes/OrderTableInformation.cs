namespace DistributionCenter.Application.Tables.Components.Information.Concretes;

using Bases;

public class OrderTableInformation : BaseEntityTableInformation
{
    public OrderTableInformation()
        : base() { }

    protected override string ObtainGetByIdFields()
    {
        return "status AS Status, route_id AS RouteId, client_id AS ClientId, delivery_point_id AS DeliveryPointId, " +
               "delivery_time AS DeliveryTime";
    }

    protected override string ObtainTableName()
    {
        return "client_order";
    }

    protected override string ObtainCreateFields()
    {
        return "status, route_id, client_id, delivery_point_id, delivery_time";
    }

    protected override string ObtainCreateValues()
    {
        return "@Status, @RouteId, @ClientId, @DeliveryPointId, @DeliveryTime";
    }

    protected override string ObtainUpdateFields()
    {
        return "status = @Status, route_id = @RouteId, client_id = @ClientId, delivery_point_id = @DeliveryPointId" +
               ", delivery_time = @DeliveryTime";
    }
}
