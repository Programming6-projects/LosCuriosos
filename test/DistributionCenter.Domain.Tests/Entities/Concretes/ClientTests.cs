namespace DistributionCenter.Domain.Tests.Entities.Concretes;

public class ClientTests
{
    [Fact]
    public void Test_Client()
    {
        // Define Input and output
        string expectedName = "Client Name";
        string expectedLastName = "Client LastName";
        string expectedEmail = "Client Email";
        Client entity =
            new()
            {
                Name = expectedName,
                LastName = expectedLastName,
                Email = expectedEmail,
            };

        // Execute actual operation
        string name = entity.Name;
        string lastName = entity.LastName;
        string email = entity.Email;

        // Verify actual result
        Assert.Equal(expectedName, name);
        Assert.Equal(expectedLastName, lastName);
        Assert.Equal(expectedEmail, email);
    }
}
