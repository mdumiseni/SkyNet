namespace SkyNetAPI.Domain.Entities;

public class BaseEntity
{
    public DateTime CreateOnDateTime { get; set; }
    public DateTime? ModifiedOnDateTime { get; set; }
    public bool IsActive { get; set; }
}