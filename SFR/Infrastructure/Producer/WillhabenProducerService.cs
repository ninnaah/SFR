using Confluent.Kafka;
using SFR.Models;
using SFR.Serialisation;
using SFR.Services.Interfaces;

namespace SFR.Infrastructure.Producer
{
    public class WillhabenProducerService
    {
        private readonly IWillhabenItemService _willhabenItemService;

        public WillhabenProducerService(IWillhabenItemService willhabenItemService)
        {
            _willhabenItemService = willhabenItemService;
        }

        public async Task Produce(int messageCount = 10, string topicName = "willhaben-items")
        {
            Console.WriteLine($"Producing {messageCount} Willhaben Items...");

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:29092"
            };

            using var producer = new ProducerBuilder<string, WillhabenItem>(producerConfig)
                .SetValueSerializer(new JsonSerializer<WillhabenItem>())
                .Build();

            var items = _willhabenItemService.GetRandomItems(messageCount);

            foreach (var item in items)
            {
                var key = $"{item.Address4}-{item.Category}";

                var message = new Message<string, WillhabenItem>
                {
                    Key = key,
                    Value = item
                };

                var result = await producer.ProduceAsync(topicName, message);
                Console.WriteLine($"Produced Willhaben Item: Key={key}, Title={item.Title}");
            }
        }
    }
}