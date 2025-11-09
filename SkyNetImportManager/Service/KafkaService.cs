using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace SkyNetImportManager.Service;

public class KafkaService
{
    private readonly string _bootstrapServers;

    public KafkaService(string bootstrapServers)
    {
        _bootstrapServers = bootstrapServers;
    }
    
    /// <summary>
    /// Creates a Kafka topic if it doesn't exist
    /// </summary>
    public async Task<string> CreateTopicAsync(string topicName, int partitions = 1, short replicationFactor = 1)
    {
        using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _bootstrapServers }).Build();

        try
        {
            await adminClient.CreateTopicsAsync(new TopicSpecification[]
            {
                new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = partitions,
                    ReplicationFactor = replicationFactor
                }
            });

            Console.WriteLine($"Topic '{topicName}' created successfully!");
        }
        catch (CreateTopicsException e)
        {
            if (e.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
            {
                Console.WriteLine($"Topic '{topicName}' already exists.");
            }
            else
            {
                Console.WriteLine($"Error creating topic '{topicName}': {e.Results[0].Error.Reason}");
            }
        }
        
        return topicName;
    }
    
    public async Task ProduceMessagesAsync(string topicName, IEnumerable<string> messages)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _bootstrapServers,
            CompressionType = CompressionType.Snappy
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        // Produce all messages in parallel
        var tasks = messages.Select(msg => 
            producer.ProduceAsync(topicName, new Message<Null, string> { Value = msg })
        );

        var results = await Task.WhenAll(tasks);
    }
}