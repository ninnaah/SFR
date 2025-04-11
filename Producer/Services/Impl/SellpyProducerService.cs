using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Producer.Models.Mapper;
using Shared.AvroModels.V1;
using Shared.Kakfaconfig;


namespace Producer.Services.Impl
{
    public class SellpyProducerService
    {
        private readonly ISellpyItemService _sellpyItemService;

        public SellpyProducerService(ISellpyItemService sellpyItemService)
        {
            _sellpyItemService = sellpyItemService;
        }

        public async Task Produce(int messageCount = KafkaSettings.DefaultMessageCount, string topicName = KafkaSettings.ClothingAdAvroTopic)
        {
            Console.WriteLine($"Producing {messageCount} Sellpy Items (Avro)...");

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

            var items = _sellpyItemService.GetRandomItems(messageCount);

            foreach (var item in items)
            {
                var avroItem = SellpyItemMapper.ToClothingAdAvro(item);

                var key = item.ObjectId ?? Guid.NewGuid().ToString();

                var message = new Message<string, ClothingAdAvro>
                {
                    Key = key,
                    Value = avroItem
                };

                var result = await producer.ProduceAsync(topicName, message);

                Console.WriteLine($"Produced Avro Sellpy Item: Key={key}, Title={avroItem.Title}");
            }
        }
    }
}