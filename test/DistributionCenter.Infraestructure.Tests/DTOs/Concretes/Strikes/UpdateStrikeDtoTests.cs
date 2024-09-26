namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Strikes;

using DistributionCenter.Commons.Results;
using DistributionCenter.Domain.Entities.Concretes;
using DistributionCenter.Infraestructure.DTOs.Concretes.Strikes;
using Xunit;

public class UpdateStrikeDtoTest
{
    [Fact]
    public void VerifyThanStrikeDescriptionIsValid()
    {
        // Define Input and Output
        UpdateStrikeDto dto = new() { Description = "Valid description", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionIsInvalid()
    {
        // Define Input and Output
        UpdateStrikeDto dto = new() { Description = "", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionHasAtLeast3Characters()
    {
        // Define Input and Output
        UpdateStrikeDto dto = new() { Description = "a", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void VerifyThanStrikeDescriptionHasLeastThan128Characters()
    {
        // Define Input and Output
        UpdateStrikeDto dto = new() { Description = new string('a', 129), TransportId = Guid.NewGuid() };

        // Execute actual operation
        Result result = dto.Validate();

        // Verify actual result
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void VerifyFromEntityMethodWithValidDescription()
    {
        // Define Input and Output
        UpdateStrikeDto dto = new() { Description = "Updated description", TransportId = null };

        Strike entity = new() { Description = "Original description", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Strike updatedEntity = dto.FromEntity(entity);

        // Verify actual result
        Assert.Equal(dto.Description, updatedEntity.Description);
        Assert.Equal(entity.TransportId, updatedEntity.TransportId);
    }

    [Fact]
    public void VerifyFromEntityMethodWithValidTransportId()
    {
        // Define Input and Output
        UpdateStrikeDto dto = new() { Description = null, TransportId = Guid.NewGuid() };

        Strike entity = new() { Description = "Original description", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Strike updatedEntity = dto.FromEntity(entity);

        // Verify actual result
        Assert.Equal(entity.Description, updatedEntity.Description);
        Assert.Equal(dto.TransportId, updatedEntity.TransportId);
    }

    [Fact]
    public void VerifyFromEntityMethodWithBothFields()
    {
        // Define Input and Output
        UpdateStrikeDto dto = new() { Description = "Updated description", TransportId = Guid.NewGuid() };

        Strike entity = new() { Description = "Original description", TransportId = Guid.NewGuid() };

        // Execute actual operation
        Strike updatedEntity = dto.FromEntity(entity);

        // Verify actual result
        Assert.Equal(dto.Description, updatedEntity.Description);
        Assert.Equal(dto.TransportId, updatedEntity.TransportId);
    }
}
