using System.Text.Json;
using Confluent.Kafka;
using Newtonsoft.Json;
using SkyNetAPI.Application;
using SkyNetAPI.Application.EntityMappers;
using SkyNetAPI.Domain.Entities;
using SkyNetAPI.ViewModels;

namespace SkyNetAPI.Infrastructure.KafkaService;

public class KafkaConsumerService
{
    private readonly string _bootstrapServers;
    private readonly string _topic;
    private readonly string _groupId;
    private readonly IWaybillRepository _waybillRepository;
    
    public KafkaConsumerService(string bootstrapServers, string topic, string groupId, IWaybillRepository waybillRepository)
    {
        _bootstrapServers = bootstrapServers;
        _topic = topic;
        _groupId = groupId;
        _waybillRepository = waybillRepository;
    }
    
    public async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _bootstrapServers,
            GroupId = _groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(_topic);

        Console.WriteLine($"Kafka Consumer started. Listening to topic: {_topic}");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(cancellationToken);
                    if (result?.Message != null)
                    {
                        Console.WriteLine($"Received: {result.Message.Value}");
                        await HandleMessageAsync(result.Message.Value);
                    }
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Consume error: {ex.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Consumer cancelled.");
        }
        finally
        {
            consumer.Close();
        }
    }
    
    private async Task HandleMessageAsync(string message)
    {
        var waybillsDto = JsonConvert.DeserializeObject<WaybillDto>(message);

        if (waybillsDto != null)
        {
            var waybill = waybillsDto.ToEntity();
            var parcelsDto = waybillsDto.ParcelInfo.ToDto();

            var parcels = new List<Parcel>();
            parcels.Add(waybillsDto.ParcelInfo.ToEntity());

            waybill.Parcels = parcels;
            var result = await _waybillRepository.AddWaybill(waybill);
        
            Console.WriteLine($"Processing message: {message}");
        }
    }
}