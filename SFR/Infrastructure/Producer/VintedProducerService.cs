using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using SFR.AvroModels.V1;
using SFR.Models.Mapper;
using SFR.Services.Interfaces;
using SFR.Configuration;

namespace SFR.Infrastructure.Producer
{
    public class VintedProducerService
    {
        private readonly IVintedItemService _vintedItemService;

        public VintedProducerService(IVintedItemService vintedItemService)
        {
            _vintedItemService = vintedItemService;
        }

        public async Task Produce(int messageCount = KafkaSettings.DefaultMessageCount, string topicName = KafkaSettings.ClothingAdAvroTopic)
        {
            Console.WriteLine($"Producing {messageCount} Vinted Items (Avro)...");

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = KafkaSettings.SchemaRegistryUrl
            };

            using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            var avroSerializer = new AvroSerializer<ClothingAdAvro>(schemaRegistry);

            using var producer = new ProducerBuilder<string, ClothingAdAvro>(producerConfig)
                .SetValueSerializer(avroSerializer)
                .Build();

            var items = _vintedItemService.GetRandomItems(messageCount);

            foreach (var item in items)
            {
                var avroItem = VintedItemMapper.ToClothingAdAvro(item);

                var key = item.ItemReference ?? Guid.NewGuid().ToString();

                var message = new Message<string, ClothingAdAvro>
                {
                    Key = key,
                    Value = avroItem
                };

                var result = await producer.ProduceAsync(topicName, message);

                Console.WriteLine($"Produced Avro Vinted Item: Key={key}, Title={avroItem.Title}");
            }
        }
    }
}