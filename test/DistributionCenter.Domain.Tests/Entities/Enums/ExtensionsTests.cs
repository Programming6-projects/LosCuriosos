namespace DistributionCenter.Domain.Tests.Entities.Enums;

using System.ComponentModel;
using DistributionCenter.Domain.Entities.Enums;

public class ExtensionsTests
{
    private enum TestEnum
    {
        [Description("First Value Description")]
        FirstValue,
        SecondValue,
    }

    [Fact]
    public void GetDescription_ReturnsDescription_WhenDescriptionAttributeIsPresent()
    {
        // Arrange
        TestEnum value = TestEnum.FirstValue;
        string expectedDescription = "First Value Description";

        // Act
        string description = value.GetDescription();

        // Assert
        Assert.Equal(expectedDescription, description);
    }

    [Fact]
    public void GetDescription_ReturnsEnumName_WhenDescriptionAttributeIsNotPresent()
    {
        // Arrange
        TestEnum value = TestEnum.SecondValue;
        string expectedDescription = "SecondValue";

        // Act
        string description = value.GetDescription();

        // Assert
        Assert.Equal(expectedDescription, description);
    }

    [Fact]
    public void GetDescription_ReturnsEmptyString_WhenFieldInfoIsNull()
    {
        // Arrange
        TestEnum value = (TestEnum)999; // Invalid enum value

        // Act
        string description = value.GetDescription();

        // Assert
        Assert.Equal(string.Empty, description);
    }
}
