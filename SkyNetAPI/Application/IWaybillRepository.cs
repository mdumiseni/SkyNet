using SkyNetAPI.Domain.Entities;
using SkyNetAPI.Infrastructure.KafkaService.Models;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Application;

public interface IWaybillRepository
{
    Task<Waybill?> GetWaybillByIdAsync(Guid id);
    Task<bool> AddWaybills(List<Waybill> waybills);
    Task<Guid> AddWaybill(Waybill waybill);
    Task<Waybill?> GetWaybillByWaybillNumberAsync(string waybillNumber);
    Task<bool> AddWaybillParcelItemsAsync(List<Parcel> parcels, Guid waybillId);
    Task<Guid> AddWaybillData(WaybillData waybillData);
}