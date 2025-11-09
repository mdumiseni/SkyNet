using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SkyNetAPI.Domain.Entities;

[Index(nameof(ParcelNumber))] 
[Table("Parcel")]
public class Parcel : BaseEntity
{
    public Guid Id { get; set; }
    public string ParcelNumber { get; set; }
    
    [Precision(18, 4)]
    public decimal Length { get; set; }

    [Precision(18, 4)]
    public decimal Breadth { get; set; }

    [Precision(18, 4)]
    public decimal Height { get; set; }

    [Precision(18, 4)]
    public decimal Mass { get; set; }

    public Guid WaybillId { get; set; }
    public Waybill Waybill { get; set; }
}