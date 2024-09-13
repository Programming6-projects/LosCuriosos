namespace DistributionCenter.Application.Tests.Tables.Components.Information.Concretes;

using DistributionCenter.Application.Tables.Components.Information.Concretes;
using Xunit;

public class TransportTableInformationTests
{
    private readonly TransportTableInformation _table;

    public TransportTableInformationTests()
    {
        _table = new TransportTableInformation();
    }

    [Fact]
    public void TableName_ReturnsExpectedString()
    {
        // Define Input and Output
        string expected = "transport";

        // Execute actual operation
        string result = _table.TableName;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetByIdFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string expected = "id AS Id, name, capacity, availableUnits, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";

        // Execute actual operation
        string result = _table.GetByIdFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CreateFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string expected = "id, name, capacity, availableUnits, is_active, created_at, updated_at";

        // Execute actual operation
        string result = _table.CreateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CreateValues_ReturnsExpectedString()
    {
        // Define Input and Output
        string expected = "@Id, @Name, @Capacity, @AvailableUnits, @IsActive, @CreatedAt, @UpdatedAt";

        // Execute actual operation
        string result = _table.CreateValues;

        // Verify actual result
        Assert.Equal(expected, result);
    }

    [Fact]
    public void UpdateFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string expected = "name = @Name, capacity = @Capacity, availableUnits = @AvailableUnits, is_active = @IsActive, updated_at = @UpdatedAt";

        // Execute actual operation
        string result = _table.UpdateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }
}
