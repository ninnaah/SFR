using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Consumer.Database;
using Consumer.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.AvroModels.V1;
using Shared.Kakfaconfig;

namespace Consumer.Services
{
    public class MultiTopicConsumer
    {
        private DbContext _dbContext;

        public MultiTopicConsumer(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Consume()
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers,
                GroupId = "multi-topic-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = KafkaSettings.SchemaRegistryUrl
            };

            using var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

            using var adConsumer = new ConsumerBuilder<string, ClothingAdAvro>(consumerConfig)
                .SetValueDeserializer(new AvroDeserializer<ClothingAdAvro>(schemaRegistry).AsSyncOverAsync())
                .Build();

            using var countConsumer = new ConsumerBuilder<string, long>(consumerConfig)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Int64)
                .Build();

            adConsumer.Subscribe(KafkaSettings.ClothingAdAvroTopic);
            countConsumer.Subscribe(KafkaSettings.CategoryCountTopic);

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
            Console.WriteLine($"[{ad.Source}] {ad.Title} ({ad.Category}) - {ad.Price} {ad.Currency}");

            _dbContext.Add(new ClothingAd()
            {
                Id = Guid.NewGuid().ToString(),
                Source = ad.Source,
                Title = ad.Title,
                Category = ad.Category,
                Price = ad.Price,
                Currency = ad.Currency
            });
            _dbContext.SaveChanges();
        }

        private void ConsumeCategoryCount(IConsumer<string, long> consumer)
        {
            var result = consumer.Consume(TimeSpan.FromMilliseconds(500));
            if (result?.Message == null) return;

            Console.WriteLine($"Kategorie: {result.Message.Key} | Neue Anzeigen: {result.Message.Value}");
        }
    }
}