using Confluent.Kafka;
using SFR.Models;
using SFR.Serialisation;

namespace SFR;

public class ProducerService
{
    public async Task Produce()
    {
        Console.WriteLine("Producing Clothing Ad Messages...");
        
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:29092",
            SecurityProtocol = SecurityProtocol.Plaintext
        };
        using var producer = new ProducerBuilder<string, ClothingAd>(producerConfig)
            .SetValueSerializer(new JsonSerializer<ClothingAd>()) 
            .Build();

        for (var i = 0; i < 10; i++)
        {
            var message = new Message<string, ClothingAd>
            {
                Key = i.ToString(),
                Value = new ClothingAd("a nice tshirt", 15.99, "Perfect condition")
            };
            var result = await producer.ProduceAsync("clothing-ad", message);
        
            Console.WriteLine($"Produced Clothing Ad: {message.Key} - {result.Message.Value.Name}");
        }
    }
}