namespace DistributionCenter.Domain.Tests.Entities.Concretes;

using DistributionCenter.Domain.Entities.Concretes;
using Xunit;

public class TransportTests
{
    [Fact]
    public void Test_Transport()
    {
        // Define input and expected output
        string expectedName = "Truck A";
        int expectedCapacity = 100;
        int expectedAvailableUnits = 50;

        // Create a new instance of Transport
        Transport entity = new()
        {
            Name = expectedName,
            Capacity = expectedCapacity,
            AvailableUnits = expectedAvailableUnits
        };

        // Execute actual operation
        string name = entity.Name;
        int capacity = entity.Capacity;
        int availableUnits = entity.AvailableUnits;

        // Verify actual result
        Assert.Equal(expectedName, name);
        Assert.Equal(expectedCapacity, capacity);
        Assert.Equal(expectedAvailableUnits, availableUnits);
    }
}
