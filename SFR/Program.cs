using SFR.Infrastructure.Consumer;
using SFR.Infrastructure.Producer;
using SFR.Services.Implementations;
using SFR.Configuration;

namespace SFR;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var willhabenMock = new WillhabenItemMockService();
        var vintedMock = new VintedItemMockService();
        var sellpyMock = new SellpyItemMockService();

        var willhabenProducer = new WillhabenProducerService(willhabenMock);
        var vintedProducer = new VintedProducerService(vintedMock);
        var sellpyProducer = new SellpyProducerService(sellpyMock);

        // Einheitliches Avro-Topic nutzen
        await willhabenProducer.Produce(KafkaSettings.DefaultMessageCount, KafkaSettings.ClothingAdAvroTopic);
        await vintedProducer.Produce(KafkaSettings.DefaultMessageCount, KafkaSettings.ClothingAdAvroTopic);
        await sellpyProducer.Produce(KafkaSettings.DefaultMessageCount, KafkaSettings.ClothingAdAvroTopic);

        var consumer = new MultiTopicConsumer();
        consumer.Consume();
    }
}