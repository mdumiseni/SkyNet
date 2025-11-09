using SkyNetAPI.Domain.Entities;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Application.EntityMappers;

public static class WaybillMapper
{
    public static WaybillDto ToDto(this Waybill waybill)
    {
        if (waybill == null) return null!; // or throw exception

        return new WaybillDto
        {
            Id = waybill.Id,
            WaybillNumber = waybill.WaybillNumber,
            ServiceType = waybill.ServiceType,
            SenderSuburb = waybill.SenderSuburb,
            SenderPostalCode = waybill.SenderPostalCode,
            RecipientSuburb = waybill.RecipientSuburb,
            RecipientPostalCode = waybill.RecipientPostalCode,
        };
    }

    public static Waybill ToEntity(this WaybillDto waybillDto)
    {
        return new Waybill
        {
            Id = Guid.NewGuid(),
            WaybillNumber = waybillDto.WaybillNumber,
            ServiceType = waybillDto.ServiceType,
            SenderSuburb = waybillDto.SenderSuburb,
            SenderPostalCode = waybillDto.SenderPostalCode,
            RecipientSuburb = waybillDto.RecipientSuburb,
            RecipientPostalCode = waybillDto.RecipientPostalCode,
            CreateOnDateTime = DateTime.UtcNow,
            IsActive = true
        };
    }
}