namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.Trip;

using Domain.Entities.Concretes;
using Domain.Entities.Enums;
using Infraestructure.DTOs.Concretes.Trip;

public class UpdateTripDtoTests
{
    [Fact]
    public void FromEntity_UpdatesAndReturnsCorrectClient()
    {
        // Define Input and Output
        Trip trip =
            new()
            {
                Status = Status.Pending,
                TransportId = new Guid()
            };
        UpdateTripDto dto =
            new()
            {
                Status = "Pending",
                TransportId = new Guid()
            };

        // Execute actual operation
        Trip updatedTrip = dto.FromEntity(trip);

        // Verify actual result
        _ = Enum.TryParse(dto.Status, true, out Status parsedStatus);
        Assert.Equal(parsedStatus, updatedTrip.Status);
        Assert.Equal(dto.TransportId, updatedTrip.TransportId);
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectClient()
    {
        // Define Input and Output
        Guid tripId = Guid.NewGuid();
        Trip trip =
            new()
            {
                Status = Status.Pending,
                TransportId = tripId
            };
        UpdateTripDto dto = new();

        // Execute actual operation
        Trip updatedTrip = dto.FromEntity(trip);

        // Verify actual result
        _ = Enum.TryParse(dto.Status, true, out Status parsedStatus);
        Assert.Equal(parsedStatus, updatedTrip.Status);
        Assert.Equal(trip.TransportId, updatedTrip.TransportId);
    }
}
