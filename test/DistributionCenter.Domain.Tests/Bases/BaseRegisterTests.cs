namespace DistributionCenter.Domain.Tests.Bases;

using DistributionCenter.Domain.Bases;

public class BaseRegisterTests
{
    [Fact]
    public void Test_Base_Register()
    {
        // Define Input and output
        DateTime? expectedUpdatedAt = null;
        Mock<BaseRegister> entityMock = new() { CallBase = true };

        // Execute actual operation
        DateTime createdAt = entityMock.Object.CreatedAt;
        DateTime? updatedAt = entityMock.Object.UpdatedAt;

        // Verify actual result
        Assert.True(createdAt <= DateTime.Now);
        Assert.Equal(expectedUpdatedAt, updatedAt);
    }
}
