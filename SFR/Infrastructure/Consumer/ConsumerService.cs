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
            Console.WriteLine("🚀 Starting Avro Multi-Topic Consumer...");

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers,
                GroupId = "multi-topic-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = KafkaSettings.SchemaRegistryUrl
            };

            using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            // Consumer für ClothingAdAvro
            using var adConsumer = new ConsumerBuilder<string, ClothingAdAvro>(consumerConfig)
                .SetValueDeserializer(new AvroDeserializer<ClothingAdAvro>(schemaRegistry).AsSyncOverAsync())
                .Build();

            // Consumer für Category Counts (key = string, value = long)
            using var countConsumer = new ConsumerBuilder<string, long>(consumerConfig)
                .SetValueDeserializer(Deserializers.Int64)
                .Build();

            adConsumer.Subscribe(KafkaSettings.ClothingAdAvroTopic);
            countConsumer.Subscribe(KafkaSettings.CategoryCountTopic);

            Console.WriteLine($"✅ Subscribed to topics:");
            Console.WriteLine($"- {KafkaSettings.ClothingAdAvroTopic}");
            Console.WriteLine($"- {KafkaSettings.CategoryCountTopic}");

            // Event Loop
            while (true)
            {
                ConsumeAd(adConsumer);
                ConsumeCategoryCount(countConsumer);
            }
        }

        private void ConsumeAd(IConsumer<string, ClothingAdAvro> consumer)
        {
            var result = consumer.Consume(TimeSpan.FromMilliseconds(100));

            if (result?.Message?.Value == null) return;

            var ad = result.Message.Value;

            Console.WriteLine($"🛒 [{ad.Source}] {ad.Title} ({ad.Category}) - {ad.Price} {ad.Currency}");
        }

        private void ConsumeCategoryCount(IConsumer<string, long> consumer)
        {
            var result = consumer.Consume(TimeSpan.FromMilliseconds(100));

            if (result?.Message == null) return;

            Console.WriteLine($"📊 Category Count | Category: {result.Message.Key} | Count: {result.Message.Value}");
        }
    }
}