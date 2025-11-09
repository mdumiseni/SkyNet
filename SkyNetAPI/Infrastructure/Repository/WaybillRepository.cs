using Microsoft.EntityFrameworkCore;
using SkyNetAPI.Application;
using SkyNetAPI.Domain.Entities;
using SkyNetAPI.Infrastructure.EntityFramework;
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

    public async Task<bool> AddWaybill(Waybill waybill)
    {
        await _context.Waybills.AddAsync(waybill);
        return await _context.SaveChangesAsync() > 0;
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
}