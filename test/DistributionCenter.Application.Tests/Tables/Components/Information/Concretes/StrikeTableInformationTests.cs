namespace DistributionCenter.Application.Tests.Tables.Components.Information.Concretes;

public class StrikeTableInformationTests
{
    private readonly StrikeTableInformation _table;

    public StrikeTableInformationTests()
    {
        _table = new StrikeTableInformation();
    }

    [Fact]
    public void TableName_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected = "strike";

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
            "id AS Id, description, transport_id AS TransportId, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";

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
        string expected = "id, description, transport_id, is_active, created_at, updated_at";

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
        string expected = "@Id, @Description, @TransportId, @IsActive, @CreatedAt, @UpdatedAt";

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
            "description = @Description, transport_id = @TransportId, is_active = @IsActive, updated_at = @UpdatedAt";

        // Execute actual operation
        result = _table.UpdateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }
}
