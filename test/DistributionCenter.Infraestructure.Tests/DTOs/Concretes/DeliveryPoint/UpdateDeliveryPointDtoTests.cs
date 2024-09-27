namespace DistributionCenter.Infraestructure.Tests.DTOs.Concretes.DeliveryPoint;

using Domain.Entities.Concretes;
using Infraestructure.DTOs.Concretes.DeliveryPoint;

public class UpdateDeliveryPointDtoTests
{
    [Fact]
    public void FromEntity_UpdatesAndReturnsCorrectDeliveryPoint()
    {
        // Define Input and Output
        DeliveryPoint deliveryPoint =
            new()
            {
                Latitude = -17.414379,
                Longitude = -66.165308,
            };
        UpdateDeliveryPointDto dto =
            new()
            {
                Latitude = -17.411414,
                Longitude = -66.167917,
            };

        // Execute actual operation
        DeliveryPoint updatedDeliveryPoint = dto.FromEntity(deliveryPoint);

        // Verify actual result
        Assert.Equal(dto.Latitude, updatedDeliveryPoint.Latitude);
        Assert.Equal(dto.Longitude, updatedDeliveryPoint.Longitude);
    }

    [Fact]
    public void FromEntity_UpdatesWithNullsAndReturnsCorrectDeliveryPoint()
    {
        // Define Input and Output
        DeliveryPoint deliveryPoint =
            new()
            {
                Latitude = -17.414379,
                Longitude = -66.165308,
            };
        UpdateDeliveryPointDto dto = new();

        // Execute actual operation
        DeliveryPoint updatedDeliveryPoint = dto.FromEntity(deliveryPoint);

        // Verify actual result
        Assert.Equal(deliveryPoint.Latitude, updatedDeliveryPoint.Latitude);
        Assert.Equal(deliveryPoint.Longitude, updatedDeliveryPoint.Longitude);
    }

    [Fact]
    public void FromEntity_ShouldNotUpdateLatitude_WhenLatitudeNotProvided()
    {
        UpdateDeliveryPointDto dto = new ()
        {
            Longitude = -74.0060
        };
        DeliveryPoint existingEntity = new ()
        {
            Latitude = 34.0522,
            Longitude = -118.2437
        };

        DeliveryPoint updatedEntity = dto.FromEntity(existingEntity);

        // Assert
        Assert.Equal(existingEntity.Latitude, updatedEntity.Latitude);
        Assert.Equal(dto.Longitude, updatedEntity.Longitude);
    }

    [Fact]
    public void FromEntity_ShouldNotUpdateLongitude_WhenLongitudeNotProvided()
    {
        UpdateDeliveryPointDto dto = new ()
        {
            Latitude = 40.7128
        };
        DeliveryPoint existingEntity = new ()
        {
            Latitude = 34.0522,
            Longitude = -118.2437
        };

        DeliveryPoint updatedEntity = dto.FromEntity(existingEntity);

        // Assert
        Assert.Equal(dto.Latitude, updatedEntity.Latitude);
        Assert.Equal(existingEntity.Longitude, updatedEntity.Longitude);
    }

    [Fact]
    public void FromEntity_ShouldNotUpdateAnyField_WhenNoValuesProvided()
    {
        UpdateDeliveryPointDto dto = new ();
        DeliveryPoint existingEntity = new ()
        {
            Latitude = 34.0522,
            Longitude = -118.2437
        };

        DeliveryPoint updatedEntity = dto.FromEntity(existingEntity);

        // Assert
        Assert.Equal(existingEntity.Latitude, updatedEntity.Latitude);
        Assert.Equal(existingEntity.Longitude, updatedEntity.Longitude);
    }
}
