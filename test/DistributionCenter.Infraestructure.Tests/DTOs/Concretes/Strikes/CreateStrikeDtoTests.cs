namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Strikes;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Concretes.Strikes;

public class CreateStrikeDtoTest
{
    [Fact]
    public void VerifyThanStrikeDescriptionIsValid()
    {
        // Define Input and Output
        CreateStrikeDto dto = new() { Description = "Valid description", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionIsInvalid()
    {
        // Define Input and Output
        CreateStrikeDto dto = new() { Description = "", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionHasAtLeast3Characters()
    {
        // Define Input and Output
        CreateStrikeDto dto = new() { Description = "a", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionHasLeastThan128Characters()
    {
        // Define Input and Output
        CreateStrikeDto dto = new() { Description = new string('a', 129), TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void VerifyToEntityMethod()
    {
        // Define Input and Output
        CreateStrikeDto dto = new() { Description = "Valid description", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Strike entity = dto.ToEntity();

        // Verify actual result
        Assert.Equal(dto.Description, entity.Description);
        Assert.Equal(dto.TransportId, entity.TransportId);
    }
}
