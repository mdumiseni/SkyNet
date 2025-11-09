using System.Globalization;
using SkyNetImportManager.CsvMapper;
using SkyNetImportManager.Model;

namespace SkyNetImportManager.Service;

public class WaybillService
{
    private readonly KafkaService _kafkaService;

    public WaybillService(KafkaService kafkaService)
    {
        _kafkaService = kafkaService;
    }

    public async Task<bool> BulkImportCsv(string csvPath, string topicName)
    {
        using var reader = new StreamReader(csvPath);
        
        using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Delimiter = "|"
        });
        
        csv.Context.RegisterClassMap<CsvWaybillMap>();
        
        var records = csv.GetRecords<Waybill>();

        var batchSize = 10;
        var batch = new List<string>(batchSize);
        
        foreach (var record in records)
        {
            var message = System.Text.Json.JsonSerializer.Serialize(record);
            batch.Add(message);
            
            if (batch.Count >= batchSize)
            {
                await _kafkaService.ProduceMessagesAsync(topicName, batch);
                batch.Clear();
            }
        }

        return true;
    }
}