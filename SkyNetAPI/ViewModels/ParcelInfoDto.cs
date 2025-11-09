namespace SkyNetAPI.ViewModels;

public class ParcelInfoDto
{
    public Guid Id { get; set; }
    public string ParcelNumber { get; set; } = "";
    public decimal Length { get; set; }
    public decimal Breadth { get; set; }
    public decimal Height { get; set; }
    public decimal Mass { get; set; }
}