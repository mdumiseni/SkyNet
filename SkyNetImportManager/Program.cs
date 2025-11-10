using SkyNetImportManager.Service;

Console.WriteLine("Hello, World!");

var topic = "batch-waybills";
var bootstrapServer = "localhost:9092";

while (true)
{
    try
    {
        Console.WriteLine("Watching folder for new files...");

        var incoming = @"./Incoming/testData.csv";
        
        var processed = @"./processed/testData.csv";

        if (!File.Exists(incoming))
        {
            Console.WriteLine($"File {incoming} not found!");
            continue;
        }
        
        var kafkaService = new KafkaService(bootstrapServer); 
        await kafkaService.CreateTopicAsync(topic);

        var waybillService = new WaybillService(kafkaService);

        var result = await waybillService.BulkImportCsv(incoming, topic);

        if (!result)
        {
            Console.WriteLine("Something went wrong");
            continue;
        }
        
        var destinationFolder = Path.GetDirectoryName(processed);
        if (!string.IsNullOrEmpty(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }
        
        File.Move(incoming, processed);
        Console.WriteLine($"{incoming} is moved to {processed}");
        Console.WriteLine("Data import processor is running.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        await Task.Delay(TimeSpan.FromSeconds(10)); // retry delay
    }
}