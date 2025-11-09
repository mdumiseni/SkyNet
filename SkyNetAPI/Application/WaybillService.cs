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
        
        var dto = waybill.ToDto();
        if (waybill.Parcels.Any())
        {
            dto.ParcelInfo = dto.ParcelInfo.ToDto();
        }

        return dto;

    }
}