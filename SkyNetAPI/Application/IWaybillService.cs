using SkyNetAPI.Domain.Entities;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Application;

public interface IWaybillService
{
    Task<Waybill?> GetWaybillByIdAsync(Guid waybillId);
    Task<WaybillDto?> GetWaybillByWaybillNumber(string waybillNumber);
}