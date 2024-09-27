
namespace DistributionCenter.Application.Tests.Constants;

using Application.Constants;

public class DbConstantsTests
{
    [Fact]
    public void DefaultConnectionStringPath_ShouldHaveExpectedValue()
    {
        // Arrange
        string expected = "Database:ConnectionString:DefaultConnection";

        // Act
        string actual = DbConstants.DefaultConnectionStringPath;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TransportSchema_ShouldHaveExpectedValue()
    {
        // Arrange
        string expected = "transport";

        // Act
        string actual = DbConstants.TransportSchema;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BusinessLocation_ShouldHaveExpectedValue()
    {
        // Arrange
        double[] expected = { -17.366068, -66.175456 };

        // Act
        double[] actual = DbConstants.BusinessLocation;

        // Assert
        Assert.Equal(expected, actual);
    }
}
