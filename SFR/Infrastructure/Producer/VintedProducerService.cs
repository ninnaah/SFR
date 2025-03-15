using Confluent.Kafka;
using SFR.Models;
using SFR.Serialisation;
using SFR.Services.Interfaces;

namespace SFR.Infrastructure.Producer
{
    public class VintedProducerService
    {
        private readonly IVintedItemService _vintedItemService;

        public VintedProducerService(IVintedItemService vintedItemService)
        {
            _vintedItemService = vintedItemService;
        }

        public async Task Produce(int messageCount = 10, string topicName = "vinted-items")
        {
            Console.WriteLine($"Producing {messageCount} Vinted Items...");

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:29092"
            };

            using var producer = new ProducerBuilder<string, VintedItem>(producerConfig)
                .SetValueSerializer(new JsonSerializer<VintedItem>())
                .Build();

            var items = _vintedItemService.GetRandomItems(messageCount);

            foreach (var item in items)
            {
                var key = item.ItemReference ?? Guid.NewGuid().ToString();

                var message = new Message<string, VintedItem>
                {
                    Key = key,
                    Value = item
                };

                var result = await producer.ProduceAsync(topicName, message);
                Console.WriteLine($"Produced Vinted Item: Key={key}, Title={item.Title}");
            }
        }
    }
}