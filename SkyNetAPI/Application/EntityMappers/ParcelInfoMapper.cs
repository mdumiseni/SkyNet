using SkyNetAPI.Domain.Entities;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Application.EntityMappers;

public static class ParcelInfoMapper
{
    public static ParcelInfoDto ToDto(this ParcelInfoDto parcel)
    {
        return new ParcelInfoDto
        {
            Id = Guid.NewGuid(),
            Length = parcel.Length,
            Breadth = parcel.Breadth,
            Height = parcel.Height,
            Mass = parcel.Mass,
            ParcelNumber = parcel.ParcelNumber
        };
    }
    
    public static Parcel ToEntity(this ParcelInfoDto parcelDto)
    {
        if (parcelDto == null) return null!;

        return new Parcel
        {
            Id = Guid.NewGuid(),
            ParcelNumber = parcelDto.ParcelNumber,
            Length = parcelDto.Length,
            Breadth = parcelDto.Breadth,
            Height = parcelDto.Height,
            Mass = parcelDto.Mass,
            CreateOnDateTime = DateTime.UtcNow,
            IsActive = true
        };
    }
}