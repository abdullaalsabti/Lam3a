using Lam3a.Data.Entities;
using Lam3a.Dto;
using ServiceProvider = Lam3a.Data.Entities.ServiceProvider;

namespace Lam3a.Utils;

public static class MapperUtil
{
    public static void MapEditProviderProfile(
        ProfileDto providerProfileDto,
        ServiceProvider providerEntity
    )
    {
        providerEntity!.FirstName = providerProfileDto.FirstName;
        providerEntity.LastName = providerProfileDto.LastName;
        providerEntity.Gender = providerProfileDto.Gender;
        providerEntity.DateOfBirth = providerProfileDto.DateOfBirth;

        // Update address
        providerEntity.Address.Street = providerProfileDto.Address.Street;
        providerEntity.Address.BuildingNumber = providerProfileDto.Address.BuildingNumber;
        providerEntity.Address.Landmark = providerProfileDto.Address.Landmark;
        providerEntity.Address.MapCoordinates.Latitude = providerProfileDto
            .Address
            .Coordinates
            .Latitude;
        providerEntity.Address.MapCoordinates.Longitude = providerProfileDto
            .Address
            .Coordinates
            .Longitude;
    }

    public static void MapEditClientProfile(ProfileDto profileDto, Client clientEntity)
    {
        clientEntity!.FirstName = profileDto.FirstName;
        clientEntity.LastName = profileDto.LastName;
        clientEntity.Gender = profileDto.Gender;
        clientEntity.DateOfBirth = profileDto.DateOfBirth;

        // Update address
        clientEntity.Address.Street = profileDto.Address.Street;
        clientEntity.Address.BuildingNumber = profileDto.Address.BuildingNumber;
        clientEntity.Address.Landmark = profileDto.Address.Landmark;
        clientEntity.Address.MapCoordinates.Latitude = profileDto.Address.Coordinates.Latitude;
        clientEntity.Address.MapCoordinates.Longitude = profileDto.Address.Coordinates.Longitude;
    }
}
