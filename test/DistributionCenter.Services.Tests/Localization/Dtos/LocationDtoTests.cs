namespace DistributionCenter.Services.Tests.Localization.Dtos;

using DistributionCenter.Services.Localization.Dtos;

public class LocationDtoTests
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        // Define Input and Output
        string expectedCategory = "In City";
        double expectedDistance = 10.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_SettersAndGetters_WorkCorrectly()
    {
        // Define Input and Output
        string initialCategory = "In City";
        double initialDistance = 10.0;
        LocationDto dto = new (initialCategory, initialDistance);

        // Execute actual operation
        dto.Category = "Inter City";
        dto.DistanceFromStore = 20.0;

        // Verify actual result
        Assert.Equal("Inter City", dto.Category);
        Assert.Equal(20.0, dto.DistanceFromStore);
    }

    [Fact]
    public void Constructor_HandlesEmptyCategory()
    {
        // Define Input and Output
        string expectedCategory = string.Empty;
        double expectedDistance = 10.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Constructor_HandlesNullCategory()
    {
        // Define Input and Output
        string expectedCategory = null!;
        double expectedDistance = 10.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Constructor_HandlesNegativeDistance()
    {
        // Define Input and Output
        string expectedCategory = "In City";
        double expectedDistance = -5.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithSpecialCharacters()
    {
        // Define Input and Output
        string initialCategory = "In City";
        double initialDistance = 10.0;
        LocationDto dto = new (initialCategory, initialDistance);

        // Execute actual operation
        dto.Category = "!@#$%^&*()";
        dto.DistanceFromStore = 1234.5678;

        // Verify actual result
        Assert.Equal("!@#$%^&*()", dto.Category);
        Assert.Equal(1234.5678, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithLargeDistance()
    {
        // Define Input and Output
        string initialCategory = "In City";
        double initialDistance = 1e6;

        // Execute actual operation
        LocationDto dto = new (initialCategory, initialDistance);
        dto.DistanceFromStore = 1e9;

        // Verify actual result
        Assert.Equal(1e9, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithZeroDistance()
    {
        // Define Input and Output
        string initialCategory = "In City";
        double initialDistance = 0.0;

        // Execute actual operation
        LocationDto dto = new (initialCategory, initialDistance);
        dto.DistanceFromStore = 0.0;

        // Verify actual result
        Assert.Equal(0.0, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithDifferentDataTypes()
    {
        // Define Input and Output
        string initialCategory = "In City";
        double initialDistance = 10.0;
        LocationDto dto = new (initialCategory, initialDistance);

        // Execute actual operation
        dto.Category = "Different Category";
        dto.DistanceFromStore = 1.1;

        // Verify actual result
        Assert.Equal("Different Category", dto.Category);
        Assert.Equal(1.1, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithDefaultValues()
    {
        // Define Input and Output
        string defaultCategory = "Default Category";
        double defaultDistance = 0.0;

        // Execute actual operation
        LocationDto dto = new (defaultCategory, defaultDistance);
        dto.Category = defaultCategory;
        dto.DistanceFromStore = defaultDistance;

        // Verify actual result
        Assert.Equal(defaultCategory, dto.Category);
        Assert.Equal(defaultDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_AcceptsExtremeValues()
    {
        // Define Input and Output
        string extremeCategory = new ('a', 1000);
        double extremeDistance = double.MaxValue;

        // Execute actual operation
        LocationDto dto = new (extremeCategory, extremeDistance);
        dto.Category = extremeCategory;
        dto.DistanceFromStore = extremeDistance;

        // Verify actual result
        Assert.Equal(extremeCategory, dto.Category);
        Assert.Equal(extremeDistance, dto.DistanceFromStore);
    }
}
