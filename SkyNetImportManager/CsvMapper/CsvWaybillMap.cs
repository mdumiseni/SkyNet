using CsvHelper.Configuration;
using SkyNetImportManager.Model;

namespace SkyNetImportManager.CsvMapper;

public class CsvWaybillMap : ClassMap<Waybill>
{
    public CsvWaybillMap()
    {
        Map(m => m.WaybillNumber).Index(0);
        Map(m => m.ServiceType).Index(1);
        Map(m => m.SenderSuburb).Index(2);
        Map(m => m.SenderPostalCode).Index(3);
        Map(m => m.RecipientSuburb).Index(4);
        Map(m => m.RecipientPostalCode).Index(5);

        References<CsvParcelInfoMap>(m => m.ParcelInfo);
    } 
}