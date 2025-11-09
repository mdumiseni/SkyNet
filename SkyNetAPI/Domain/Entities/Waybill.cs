using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkyNetAPI.Domain.Entities;

[Table("Waybill")]
[Index(nameof(WaybillNumber))]
public class Waybill : BaseEntity
{
    public Guid Id { get; set; }
    public string? WaybillNumber { get; set; }

    public string ServiceType { get; set; }
    
    public string SenderSuburb { get; set; } = "";
    public string SenderPostalCode { get; set; } = "";
    
    public string RecipientSuburb { get; set; } = "";
    public string RecipientPostalCode { get; set; } = "";

    public ICollection<Parcel> Parcels { get; set; } = new List<Parcel>();
}