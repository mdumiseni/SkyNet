namespace SkyNetAPI.Infrastructure.KafkaService.Models;

public class ParcelInfoData
{
    public string ParcelNumber { get; set; } = "";
    public decimal Length { get; set; }
    public decimal Breadth { get; set; }
    public decimal Height { get; set; }
    public decimal Mass { get; set; }
}