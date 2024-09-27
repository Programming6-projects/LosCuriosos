namespace DistributionCenter.Application.Tests.Tables.Components.Information.Bases;

using DistributionCenter.Application.Tables.Components.Information.Bases;
using Moq.Protected;

public class BaseEntityTableInformationTests
{
    private readonly Mock<BaseEntityTableInformation> _mock;

    public BaseEntityTableInformationTests()
    {
        _mock = new Mock<BaseEntityTableInformation>() { CallBase = true };
    }

    [Fact]
    public void TableName_ReturnsExpectedString()
    {
        // Define Input and Output
        string testString = "TestTable";
        _ = _mock.Protected().Setup<string>("ObtainTableName").Returns(testString);

        // Execute actual operation
        string result = _mock.Object.TableName;

        // Verify actual result
        Assert.Equal(testString, result);
    }

    [Fact]
    public void GetByIdFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string testString = "TestFields";
        _ = _mock.Protected().Setup<string>("ObtainGetByIdFields").Returns(testString);

        // Execute actual operation
        string result = _mock.Object.GetByIdFields;

        // Verify actual result
        Assert.Equal(
            $"id AS Id, {testString}, is_active AS IsActive, created_at AS CreatedAt, updated_at AS UpdatedAt",
            result
        );
    }

    [Fact]
    public void CreateFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string testString = "TestFields";
        _ = _mock.Protected().Setup<string>("ObtainCreateFields").Returns(testString);

        // Execute actual operation
        string result = _mock.Object.CreateFields;

        // Verify actual result
        Assert.Equal($"id, {testString}, is_active, created_at, updated_at", result);
    }

    [Fact]
    public void CreateValues_ReturnsExpectedString()
    {
        // Define Input and Output
        string testString = "TestValues";
        _ = _mock.Protected().Setup<string>("ObtainCreateValues").Returns(testString);

        // Execute actual operation
        string result = _mock.Object.CreateValues;

        // Verify actual result
        Assert.Equal($"@Id, {testString}, @IsActive, @CreatedAt, @UpdatedAt", result);
    }

    [Fact]
    public void UpdateFields_ReturnsExpectedString()
    {
        // Define Input and Output
        string testString = "TestFields";
        _ = _mock.Protected().Setup<string>("ObtainUpdateFields").Returns(testString);

        // Execute actual operation
        string result = _mock.Object.UpdateFields;

        // Verify actual result
        Assert.Equal($"{testString}, is_active = @IsActive, updated_at = @UpdatedAt", result);
    }
}
