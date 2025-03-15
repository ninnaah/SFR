using Confluent.Kafka;
using SFR.Models;
using SFR.Serialisation;

namespace SFR.Infrastructure.Consumer
{
    public class MultiTopicConsumer
    {
        public void Consume()
        {
            Console.WriteLine("Starting Multi-Topic Consumer...");

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:29092",
                GroupId = "multi-topic-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            // Consumer für Willhaben Items
            using var willhabenConsumer = new ConsumerBuilder<string, WillhabenItem>(config)
                .SetValueDeserializer(new JsonDeserializer<WillhabenItem>())
                .Build();

            // Consumer für Vinted Items
            using var vintedConsumer = new ConsumerBuilder<string, VintedItem>(config)
                .SetValueDeserializer(new JsonDeserializer<VintedItem>())
                .Build();

            // Consumer für Sellpy Items
            using var sellpyConsumer = new ConsumerBuilder<string, SellpyItem>(config)
                .SetValueDeserializer(new JsonDeserializer<SellpyItem>())
                .Build();

            // Subscribe to topics
            willhabenConsumer.Subscribe("willhaben-items");
            vintedConsumer.Subscribe("vinted-items");
            sellpyConsumer.Subscribe("sellpy-items");

            Console.WriteLine("Subscribed to all topics. Listening...");

            while (true)
            {
                // Consume Willhaben Item
                var willhabenResult = willhabenConsumer.Consume(TimeSpan.FromMilliseconds(100));
                if (willhabenResult != null)
                {
                    var item = willhabenResult.Message.Value;
                    Console.WriteLine($"[Willhaben] Key: {willhabenResult.Message.Key} - Title: {item.Title} - Location: {item.Address4}");
                }

                // Consume Vinted Item
                var vintedResult = vintedConsumer.Consume(TimeSpan.FromMilliseconds(100));
                if (vintedResult != null)
                {
                    var item = vintedResult.Message.Value;
                    Console.WriteLine($"[Vinted] Key: {vintedResult.Message.Key} - Title: {item.Title} - Price: {item.Price}");
                }

                // Consume Sellpy Item
                var sellpyResult = sellpyConsumer.Consume(TimeSpan.FromMilliseconds(100));
                if (sellpyResult != null)
                {
                    var item = sellpyResult.Message.Value;
                    Console.WriteLine($"[Sellpy] Key: {sellpyResult.Message.Key} - Headline: {item.Headline} - Status: {item.ItemStatus}");
                }
            }
        }
    }
}