using SkyNetAPI.Application.EntityMappers;
using SkyNetAPI.Domain.Entities;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Application;

public class WaybillService : IWaybillService
{
    private readonly IWaybillRepository _waybillRepository;
    
    public WaybillService(IWaybillRepository waybillRepository)
    {
        _waybillRepository = waybillRepository;
    }
    public async Task<Waybill?> GetWaybillByIdAsync(Guid waybillId)
    {
        return await _waybillRepository.GetWaybillByIdAsync(waybillId);
    }

    public async Task<WaybillDto?> GetWaybillByWaybillNumber(string waybillNumber)
    {
        var waybill = await _waybillRepository.GetWaybillByWaybillNumberAsync(waybillNumber);

        if (waybill is null) return null;

        var dto = new WaybillDto()
        {
            WaybillNumber = waybill.WaybillNumber,
            ServiceType = waybill.ServiceType,
            SenderSuburb = waybill.SenderSuburb,
            SenderPostalCode = waybill.SenderPostalCode,
            RecipientSuburb = waybill.RecipientSuburb,
            RecipientPostalCode = waybill.RecipientPostalCode,
            ParcelInfo = waybill.Parcels.Select(x => new ParcelInfoDto()
            {
                ParcelNumber = x.ParcelNumber,
                Length = x.Length,
                Breadth = x.Breadth,
                Height = x.Height,
                Mass = x.Mass
            }).ToList()
        };
        return dto;
    }
}