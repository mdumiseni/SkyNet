using SkyNetAPI.Domain.Entities;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Application;

public interface IWaybillRepository
{
    Task<Waybill?> GetWaybillByIdAsync(Guid id);
    Task<bool> AddWaybills(List<Waybill> waybills);
    Task<bool> AddWaybill(Waybill waybill);
    Task<Waybill?> GetWaybillByWaybillNumberAsync(string waybillNumber);
}