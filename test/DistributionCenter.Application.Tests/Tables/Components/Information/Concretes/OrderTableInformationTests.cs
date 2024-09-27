namespace DistributionCenter.Application.Tests.Tables.Components.Information.Concretes;

using Application.Tables.Components.Information.Concretes;

public class OrderTableInformationTests
{
    private readonly OrderTableInformation _table;

    public OrderTableInformationTests()
    {
        _table = new OrderTableInformation();
    }

    [Fact]
    public void TableName_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected = "client_order";

        // Execute actual operation
        result = _table.TableName;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetByIdFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected =
            "id AS Id, status AS Status, route_id AS RouteId, client_id AS ClientId, delivery_point_id AS DeliveryPointId, " +
            "delivery_time AS DeliveryTime, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";

        // Execute actual operation
        result = _table.GetByIdFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CreateFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected = "id, status, route_id, client_id, delivery_point_id, delivery_time, is_active, created_at, updated_at";

        // Execute actual operation
        result = _table.CreateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CreateValues_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected = "@Id, @Status, @RouteId, @ClientId, @DeliveryPointId, @DeliveryTime, @IsActive, @CreatedAt, @UpdatedAt";

        // Execute actual operation
        result = _table.CreateValues;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void UpdateFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected =
            "status = @Status, route_id = @RouteId, client_id = @ClientId, delivery_point_id = @DeliveryPointId, " +
            "delivery_time = @DeliveryTime, is_active = @IsActive, updated_at = @UpdatedAt";

        // Execute actual operation
        result = _table.UpdateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }
}
