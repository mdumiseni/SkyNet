using Microsoft.EntityFrameworkCore;
using SkyNetAPI.Domain.Entities;

namespace SkyNetAPI.Infrastructure.EntityFramework;

public class ApplicationDbContext : DbContext
{
    public DbSet<Waybill> Waybills { get; set; }
    public DbSet<Parcel> Parcels { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Port=5432;Database=SkyNet_db;Username=postgres;Password=Mdumiseni@96;";
        optionsBuilder.UseNpgsql(connectionString);
    }
}