using CsvHelper.Configuration;
using SkyNetImportManager.Model;

namespace SkyNetImportManager.CsvMapper;

public class CsvParcelInfoMap : ClassMap<ParcelInfo>
{
    public CsvParcelInfoMap()
    {
        Map(m => m.ParcelNumber).Index(6);
        Map(m => m.Length).Index(7);
        Map(m => m.Breadth).Index(8);
        Map(m => m.Height).Index(9);
        Map(m => m.Mass).Index(10);
    }
}