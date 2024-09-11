namespace DistributionCenter.Application.Tests.Tables.Components.Information.Concretes;

using DistributionCenter.Application.Tables.Components.Information.Concretes;

public class ClientTableInformationTests
{
    private readonly ClientTableInformation _table;

    public ClientTableInformationTests()
    {
        _table = new ClientTableInformation();
    }

    [Fact]
    public void TableName_ReturnsExpectedString()
    {
        // Define Input and Output
        string result;
        string expected = "client";

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
            "id AS Id, name, last_name AS LastName, email, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt";

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
        string expected = "id, name, last_name, email, is_active, created_at, updated_at";

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
        string expected = "@Id, @Name, @LastName, @Email, @IsActive, @CreatedAt, @UpdatedAt";

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
            "name = @Name, last_name = @LastName, email = @Email, is_active = @IsActive, updated_at = @UpdatedAt";

        // Execute actual operation
        result = _table.UpdateFields;

        // Verify actual result
        Assert.Equal(expected, result);
    }
}
