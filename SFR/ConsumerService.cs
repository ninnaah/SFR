using Confluent.Kafka;
using SFR.Models;
using SFR.Serialisation;

namespace SFR;

public class ConsumerService
{
    public void Consume()
    {
        Console.WriteLine("\nConsuming Clothing Ad Messages...");
        
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "localhost:29092",
            GroupId = "clothing-ad-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            SecurityProtocol = SecurityProtocol.Plaintext
        };
        using var consumer = new ConsumerBuilder<string, ClothingAd>(consumerConfig)
            .SetValueDeserializer(new JsonDeserializer<ClothingAd>()) 
            .Build();
        
        consumer.Subscribe("clothing-ad");
    
        while (true)
        {
            var message = consumer.Consume();
            Console.WriteLine($"Received Clothing Ad: {message.Key} - {message.Value.Name}");
        }
    }
}