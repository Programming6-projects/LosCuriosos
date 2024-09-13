namespace DistributionCenter.Application.Tests.Tables.Components.Information.Concretes;

public class ProductTableInformationTests
{
    private readonly ProductTableInformation _table;

    public ProductTableInformationTests()
    {
        _table = new ProductTableInformation();
    }

    [Fact]
    public void TableName_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected = "product";

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
            "id AS Id, name, description, weight_gr AS Weight, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";

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
        string expected = "id, name, description, weight_gr, is_active, created_at, updated_at";

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
        string expected = "@Id, @Name, @Description, @Weight, @IsActive, @CreatedAt, @UpdatedAt";

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
            "name = @Name, description = @Description, weight_gr = @Weight, is_active = @IsActive, updated_at = @UpdatedAt";

        // Execute actual operation
        result = _table.UpdateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }
}
