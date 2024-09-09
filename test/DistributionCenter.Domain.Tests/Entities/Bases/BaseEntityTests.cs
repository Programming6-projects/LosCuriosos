namespace DistributionCenter.Domain.Tests.Entities.Bases;

using DistributionCenter.Domain.Entities.Bases;

public class BaseEntityTests
{
    [Fact]
    public void Test_Base_Entity()
    {
        // Define Input and output
        bool expectedIsActive = true;
        Mock<BaseEntity> entityMock = new() { CallBase = true };

        // Execute actual operation
        Guid id = entityMock.Object.Id;
        bool isActive = entityMock.Object.IsActive;

        // Verify actual result
        Assert.NotEqual(Guid.Empty, id);
        Assert.Equal(expectedIsActive, isActive);
    }
}
