namespace DistributionCenter.Domain.Tests.Concretes;

using DistributionCenter.Domain.Concretes;

public class ClientTests
{
    [Fact]
    public void Test_Client()
    {
        // Define Input and output
        string expectedName = "Client Name";
        string expectedLastName = "Client LastName";
        string expectedPhoneNumber = "Client Phone Number";
        Client entity =
            new()
            {
                Name = expectedName,
                LastName = expectedLastName,
                PhoneNumber = expectedPhoneNumber,
            };

        // Execute actual operation
        string name = entity.Name;
        string lastName = entity.LastName;
        string phoneNumber = entity.PhoneNumber;

        // Verify actual result
        Assert.Equal(expectedName, name);
        Assert.Equal(expectedLastName, lastName);
        Assert.Equal(expectedPhoneNumber, phoneNumber);
    }
}
