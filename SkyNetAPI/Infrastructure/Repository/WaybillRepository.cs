using Microsoft.EntityFrameworkCore;
using SkyNetAPI.Application;
using SkyNetAPI.Domain.Entities;
using SkyNetAPI.Infrastructure.EntityFramework;
using SkyNetAPI.Infrastructure.KafkaService.Models;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Infrastructure.Repository;

public class WaybillRepository : IWaybillRepository
{
    private readonly ApplicationDbContext _context;

    public WaybillRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Waybill?> GetWaybillByIdAsync(Guid id)
    {
        return await _context.Waybills.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> AddWaybills(List<Waybill> waybills)
    {
        await _context.Waybills.AddRangeAsync(waybills);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Guid> AddWaybill(Waybill waybill)
    {
        await _context.Waybills.AddAsync(waybill);
        var result =  await _context.SaveChangesAsync() > 0;

        return result ? waybill.Id : Guid.Empty;
    }

    public async Task<Waybill?> GetWaybillByWaybillNumberAsync(string waybillNumber)
    {
        var result = await _context.Waybills
            .Where(x => x.WaybillNumber == waybillNumber)
            .Include(x => x.Parcels)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<bool> AddWaybillParcelItemsAsync(List<Parcel> parcels, Guid waybillId)
    {
        if (parcels == null || parcels.Count == 0)
            return false;

        var updatedParcels = parcels.Select(p => new Parcel
        {
            Id = Guid.NewGuid(),
            WaybillId = waybillId,
            ParcelNumber = p.ParcelNumber,
            Length = p.Length,
            Breadth = p.Breadth,
            Height = p.Height,
            Mass = p.Mass
        }).ToList();

        await _context.Parcels.AddRangeAsync(updatedParcels);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Guid> AddWaybillData(WaybillData waybillData)
    {
        var exist = await _context.Waybills.FirstOrDefaultAsync(x =>
            x.WaybillNumber == waybillData.WaybillNumber && x.IsActive == true);

        if (exist is not null)
        {
            var parcelExist =
                await _context.Parcels.FirstOrDefaultAsync(x => x.ParcelNumber == waybillData.ParcelInfo.ParcelNumber && x.IsActive == true);

            if (parcelExist is null)
            {
                await _context.Parcels.AddAsync(new Parcel()
                {
                    Mass = waybillData.ParcelInfo.Mass,
                    WaybillId = exist.Id,
                    ParcelNumber = waybillData.ParcelInfo.ParcelNumber,
                    Length = waybillData.ParcelInfo.Length,
                    Breadth = waybillData.ParcelInfo.Breadth,
                    Height = waybillData.ParcelInfo.Height,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    CreateOnDateTime = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();

                return exist.Id;
            }
        }

        var newWaybill = new Waybill()
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            CreateOnDateTime = DateTime.UtcNow,
            WaybillNumber = waybillData.WaybillNumber,
            RecipientSuburb = waybillData.RecipientSuburb,
            SenderSuburb = waybillData.SenderSuburb,
            ServiceType = waybillData.ServiceType,
            RecipientPostalCode = waybillData.RecipientPostalCode,
            SenderPostalCode = waybillData.SenderPostalCode
        };

        await _context.Waybills.AddAsync(newWaybill);
        var result  = await _context.SaveChangesAsync() > 0;

        if (result)
        {
            await _context.Parcels.AddAsync(new Parcel()
            {
                Mass = waybillData.ParcelInfo.Mass,
                WaybillId = newWaybill.Id,
                ParcelNumber = waybillData.ParcelInfo.ParcelNumber,
                Length = waybillData.ParcelInfo.Length,
                Breadth = waybillData.ParcelInfo.Breadth,
                Height = waybillData.ParcelInfo.Height,
                Id = Guid.NewGuid(),
                IsActive = true,
                CreateOnDateTime = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
        }

        return newWaybill.Id;
    }
}