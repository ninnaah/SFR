using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Producer.Models.Mapper;
using Shared.AvroModels.V1;
using Shared.Kakfaconfig;

namespace Producer.Services.Impl
{
    public class WillhabenProducerService
    {
        private readonly IWillhabenItemService _willhabenItemService;

        public WillhabenProducerService(IWillhabenItemService willhabenItemService)
        {
            _willhabenItemService = willhabenItemService;
        }

        public async Task Produce(int messageCount = KafkaSettings.DefaultMessageCount, string topicName = KafkaSettings.ClothingAdAvroTopic)
        {
            Console.WriteLine($"Producing {messageCount} Willhaben Items (Avro)...");

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = KafkaSettings.SchemaRegistryUrl
            };

            using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            var avroSerializerConfig = new AvroSerializerConfig();

            var avroSerializer = new AvroSerializer<ClothingAdAvro>(schemaRegistry, avroSerializerConfig);

            using var producer = new ProducerBuilder<string, ClothingAdAvro>(producerConfig)
                .SetValueSerializer(avroSerializer)
                .Build();

            var items = _willhabenItemService.GetRandomItems(messageCount);

            foreach (var item in items)
            {
                var avroItem = WillhabenItemMapper.ToClothingAdAvroV1(item);

                var key = $"{item.Address4}-{item.Category}";

                var message = new Message<string, ClothingAdAvro>
                {
                    Key = key,
                    Value = avroItem
                };

                var result = await producer.ProduceAsync(topicName, message);

                Console.WriteLine($"Produced Avro Willhaben Item: Key={key}, Title={avroItem.Title}");
            }
        }
    }
}