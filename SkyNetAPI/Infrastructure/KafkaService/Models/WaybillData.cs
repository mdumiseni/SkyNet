namespace SkyNetAPI.Infrastructure.KafkaService.Models;

public class WaybillData
{
    public string WaybillNumber { get; set; } = "";
    public string ServiceType { get; set; } = "";
    
    public string SenderSuburb { get; set; } = "";
    public string SenderPostalCode { get; set; } = "";
    
    public string RecipientSuburb { get; set; } = "";
    public string RecipientPostalCode { get; set; } = "";

    public ParcelInfoData ParcelInfo { get; set; } = new ();
}