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
        string expectedPlate = "1852SBJ";
        int expectedCapacity = 9;
        int expectedCurrentCapacity = 9;
        bool expectedIsAvailable = true;

        // Create a new instance of Transport


        Transport entity = new()
        {
            Name = "Truck A",
            Plate = "1852SBJ",
            Capacity = 9,
            CurrentCapacity = 9,
            IsAvailable = true
        };

        // Execute actual operation
        string name = entity.Name;
        string plate = entity.Plate;
        int capacity = entity.Capacity;
        int currentCapacity = entity.CurrentCapacity;
        bool isAvailable = entity.IsAvailable;

        // Verify actual result
        Assert.Equal(expectedName, name);
        Assert.Equal(expectedPlate, plate);
        Assert.Equal(expectedCapacity, capacity);
        Assert.Equal(expectedCurrentCapacity, currentCapacity);
        Assert.Equal(expectedIsAvailable, isAvailable);
    }
}
