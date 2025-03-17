using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using SFR.AvroModels.V1;
using SFR.Configuration;

namespace SFR.Infrastructure.Consumer
{
    public class MultiTopicConsumer
    {
        public void Consume()
        {
            Console.WriteLine("🚀 Starting Avro Consumer...");

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers,
                GroupId = "clothing-ad-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = KafkaSettings.SchemaRegistryUrl
            };

            using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            using var consumer = new ConsumerBuilder<string, ClothingAdAvro>(consumerConfig)
                .SetValueDeserializer(new AvroDeserializer<ClothingAdAvro>(schemaRegistry).AsSyncOverAsync())
                .Build();

            consumer.Subscribe(KafkaSettings.ClothingAdAvroTopic);

            Console.WriteLine($"✅ Subscribed to topic: {KafkaSettings.ClothingAdAvroTopic}");

            while (true)
            {
                var result = consumer.Consume();
                var ad = result?.Message?.Value;

                if (ad == null) continue;

                Console.WriteLine($"🛒 [{ad.Source}] {ad.Title} ({ad.Category}) - {ad.Price} {ad.Currency}");
            }
        }
    }
}