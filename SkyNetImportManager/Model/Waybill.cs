namespace SkyNetImportManager.Model;

public class Waybill
{
    public Guid Id { get; set; }
    public string WaybillNumber { get; set; } = "";
    public string ServiceType { get; set; } = "";
    
    public string SenderSuburb { get; set; } = "";
    public string SenderPostalCode { get; set; } = "";
    
    public string RecipientSuburb { get; set; } = "";
    public string RecipientPostalCode { get; set; } = "";

    public ParcelInfo ParcelInfo { get; set; } = new ();
}