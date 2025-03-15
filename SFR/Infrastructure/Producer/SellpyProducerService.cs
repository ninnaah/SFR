using Confluent.Kafka;
using SFR.Models;
using SFR.Serialisation;
using SFR.Services.Interfaces;

namespace SFR.Infrastructure.Producer
{
    public class SellpyProducerService
    {
        private readonly ISellpyItemService _sellpyItemService;

        public SellpyProducerService(ISellpyItemService sellpyItemService)
        {
            _sellpyItemService = sellpyItemService;
        }

        public async Task Produce(int messageCount = 10, string topicName = "sellpy-items")
        {
            Console.WriteLine($"Producing {messageCount} Sellpy Items...");

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:29092"
            };

            using var producer = new ProducerBuilder<string, SellpyItem>(producerConfig)
                .SetValueSerializer(new JsonSerializer<SellpyItem>())
                .Build();

            var items = _sellpyItemService.GetRandomItems(messageCount);

            foreach (var item in items)
            {
                var key = item.ObjectId ?? Guid.NewGuid().ToString();

                var message = new Message<string, SellpyItem>
                {
                    Key = key,
                    Value = item
                };

                var result = await producer.ProduceAsync(topicName, message);
                Console.WriteLine($"Produced Sellpy Item: Key={key}, Headline={item.Headline}");
            }
        }
    }
}