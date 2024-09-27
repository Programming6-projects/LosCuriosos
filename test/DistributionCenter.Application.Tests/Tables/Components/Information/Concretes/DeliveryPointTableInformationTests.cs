namespace DistributionCenter.Application.Tests.Tables.Components.Information.Concretes;

public class DeliveryPointTableInformationTests
{
    private readonly DeliveryPointTableInformation _table;

    public DeliveryPointTableInformationTests()
    {
        _table = new DeliveryPointTableInformation();
    }

    [Fact]
    public void TableName_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected = "delivery_point";

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
            "id AS Id, latitude AS Latitude, longitude AS Longitude, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";

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
        string expected = "id, latitude, longitude, is_active, created_at, updated_at";

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
        string expected = "@Id, @Latitude, @Longitude, @IsActive, @CreatedAt, @UpdatedAt";

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
            "latitude = @Latitude, longitude = @Longitude, is_active = @IsActive, updated_at = @UpdatedAt";

        // Execute actual operation
        result = _table.UpdateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }
}
