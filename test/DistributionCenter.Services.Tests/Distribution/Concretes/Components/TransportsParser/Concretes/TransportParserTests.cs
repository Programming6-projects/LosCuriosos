namespace DistributionCenter.Services.Tests.Distribution.Concretes.Components.TransportsParser.Concretes;

using Domain.Entities.Concretes;

public class TransportParserTests
{
    [Fact]
    public void Parse_ReturnsParsedTransports_WhenTransportsAreAvailable()
    {
        // Define Input and Output
        List<Transport> transports =
        [
            new Transport
            {
                Name = "Van 1A",
                Plate = "34ABC123",
                Capacity = 7000000,
                CurrentCapacity = 30000,
                IsAvailable = true,
            },
            new Transport
            {
                Name = "Truck 2B",
                Plate = "34DEF456",
                Capacity = 50000,
                CurrentCapacity = 4000,
                IsAvailable = true,
            },
            new Transport
            {
                Name = "Van 3C",
                Plate = "34GHI789",
                Capacity = 60000,
                CurrentCapacity = 40000,
                IsAvailable = false,
            },
        ];

        TransportParser parser = new();

        // Exectual actual Operation
        List<(Trip, Transport)> result = parser.Parse(transports, Location.InCity).ToList();

        // Verify actual Result
        _ = Assert.Single(result);
        Assert.Equal(4000, result[0].Item2.CurrentCapacity);
    }

    [Fact]
    public void Parse_ReturnsParsedTransports_WhenLocationIsOutCity()
    {
        // Define Input and Output
        List<Transport> transports =
        [
            new Transport
            {
                Name = "Van 1A",
                Plate = "34ABC123",
                Capacity = 7000000,
                CurrentCapacity = 30000,
                IsAvailable = true,
            },
            new Transport
            {
                Name = "Truck 2B",
                Plate = "34DEF456",
                Capacity = 80000,
                CurrentCapacity = 60000,
                IsAvailable = true,
            },
            new Transport
            {
                Name = "Van 3C",
                Plate = "34GHI789",
                Capacity = 60000,
                CurrentCapacity = 40000,
                IsAvailable = false,
            },
        ];

        TransportParser parser = new();

        // Exectual actual Operation
        List<(Trip, Transport)> result = parser.Parse(transports, Location.OutCity).ToList();

        // Verify actual Result
        _ = Assert.Single(result);
        Assert.Equal(30000, result[0].Item2.CurrentCapacity);
    }
}
