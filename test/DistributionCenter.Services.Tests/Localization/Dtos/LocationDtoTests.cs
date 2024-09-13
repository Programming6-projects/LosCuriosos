namespace DistributionCenter.Services.Tests.Localization.Dtos;

using DistributionCenter.Services.Localization.Dtos;

public class LocationDtoTests
{
    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        // Define Input and Output
        string expectedCategory = "In City";
        bool expectedIsInCountry = true;
        double expectedDistance = 10.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedIsInCountry, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedIsInCountry, dto.IsInCountry);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_SettersAndGetters_WorkCorrectly()
    {
        // Define Input and Output
        string initialCategory = "In City";
        bool initialIsInCountry = true;
        double initialDistance = 10.0;
        LocationDto dto = new (initialCategory, initialIsInCountry, initialDistance);

        // Execute actual operation
        dto.Category = "Inter City";
        dto.IsInCountry = false;
        dto.DistanceFromStore = 20.0;

        // Verify actual result
        Assert.Equal("Inter City", dto.Category);
        Assert.False(dto.IsInCountry);
        Assert.Equal(20.0, dto.DistanceFromStore);
    }

    [Fact]
    public void Constructor_HandlesEmptyCategory()
    {
        // Define Input and Output
        string expectedCategory = string.Empty;
        bool expectedIsInCountry = true;
        double expectedDistance = 10.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedIsInCountry, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedIsInCountry, dto.IsInCountry);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Constructor_HandlesNullCategory()
    {
        // Define Input and Output
        string expectedCategory = null!;
        bool expectedIsInCountry = true;
        double expectedDistance = 10.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedIsInCountry, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedIsInCountry, dto.IsInCountry);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Constructor_HandlesNegativeDistance()
    {
        // Define Input and Output
        string expectedCategory = "In City";
        bool expectedIsInCountry = true;
        double expectedDistance = -5.0;

        // Execute actual operation
        LocationDto dto = new (expectedCategory, expectedIsInCountry, expectedDistance);

        // Verify actual result
        Assert.Equal(expectedCategory, dto.Category);
        Assert.Equal(expectedIsInCountry, dto.IsInCountry);
        Assert.Equal(expectedDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithSpecialCharacters()
    {
        // Define Input and Output
        string initialCategory = "In City";
        bool initialIsInCountry = true;
        double initialDistance = 10.0;
        LocationDto dto = new (initialCategory, initialIsInCountry, initialDistance);

        // Execute actual operation
        dto.Category = "!@#$%^&*()";
        dto.IsInCountry = false;
        dto.DistanceFromStore = 1234.5678;

        // Verify actual result
        Assert.Equal("!@#$%^&*()", dto.Category);
        Assert.False(dto.IsInCountry);
        Assert.Equal(1234.5678, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithLargeDistance()
    {
        // Define Input and Output
        string initialCategory = "In City";
        bool initialIsInCountry = true;
        double initialDistance = 1e6;

        // Execute actual operation
        LocationDto dto = new (initialCategory, initialIsInCountry, initialDistance);
        dto.DistanceFromStore = 1e9;

        // Verify actual result
        Assert.Equal(1e9, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithZeroDistance()
    {
        // Define Input and Output
        string initialCategory = "In City";
        bool initialIsInCountry = true;
        double initialDistance = 0.0;

        // Execute actual operation
        LocationDto dto = new (initialCategory, initialIsInCountry, initialDistance);
        dto.DistanceFromStore = 0.0;

        // Verify actual result
        Assert.Equal(0.0, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithDifferentDataTypes()
    {
        // Define Input and Output
        string initialCategory = "In City";
        bool initialIsInCountry = true;
        double initialDistance = 10.0;
        LocationDto dto = new (initialCategory, initialIsInCountry, initialDistance);

        // Execute actual operation
        dto.Category = "Different Category";
        dto.IsInCountry = false;
        dto.DistanceFromStore = 1.1;

        // Verify actual result
        Assert.Equal("Different Category", dto.Category);
        Assert.False(dto.IsInCountry);
        Assert.Equal(1.1, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_WorkWithDefaultValues()
    {
        // Define Input and Output
        string defaultCategory = "Default Category";
        bool defaultIsInCountry = true;
        double defaultDistance = 0.0;

        // Execute actual operation
        LocationDto dto = new (defaultCategory, defaultIsInCountry, defaultDistance);
        dto.Category = defaultCategory;
        dto.IsInCountry = defaultIsInCountry;
        dto.DistanceFromStore = defaultDistance;

        // Verify actual result
        Assert.Equal(defaultCategory, dto.Category);
        Assert.Equal(defaultIsInCountry, dto.IsInCountry);
        Assert.Equal(defaultDistance, dto.DistanceFromStore);
    }

    [Fact]
    public void Property_Setters_AcceptsExtremeValues()
    {
        // Define Input and Output
        string extremeCategory = new ('a', 1000);
        bool extremeIsInCountry = false;
        double extremeDistance = double.MaxValue;

        // Execute actual operation
        LocationDto dto = new (extremeCategory, extremeIsInCountry, extremeDistance);
        dto.Category = extremeCategory;
        dto.IsInCountry = extremeIsInCountry;
        dto.DistanceFromStore = extremeDistance;

        // Verify actual result
        Assert.Equal(extremeCategory, dto.Category);
        Assert.Equal(extremeIsInCountry, dto.IsInCountry);
        Assert.Equal(extremeDistance, dto.DistanceFromStore);
    }
}
