namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Trip;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Infraestructure.DTOs.Concretes.Trip;

public class CreateTripDtoTests
{
    [Fact]
    public void ToEntity_ReturnsCorrectClient()
    {
        // Define Input and Output
        CreateTripDto dto =
            new()
            {
                Status = "Pending",
                TransportId = new Guid()
            };

        // Execute actual operation
        Trip trip = dto.ToEntity();

        // Verify actual result
        _ = Enum.TryParse(dto.Status, true, out Status statusParsed);
        Assert.Equal(statusParsed, trip.Status);
        Assert.Equal(dto.TransportId, trip.TransportId);
    }
}
