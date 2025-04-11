
using Producer.Services.Impl;
using Producer.Services.Mocks;
using Shared.Kakfaconfig;

namespace Producer;

public static class Program
{
    public static async Task Main()
    {
        var willhabenMock = new WillhabenItemMockService();
        var vintedMock = new VintedItemMockService();
        var sellpyMock = new SellpyItemMockService();

        var willhabenProducer = new WillhabenProducerService(willhabenMock);
        var vintedProducer = new VintedProducerService(vintedMock);
        var sellpyProducer = new SellpyProducerService(sellpyMock);

        await willhabenProducer.Produce(KafkaSettings.DefaultMessageCount, KafkaSettings.ClothingAdAvroTopic);
        await vintedProducer.Produce(KafkaSettings.DefaultMessageCount, KafkaSettings.ClothingAdAvroTopic);
        await sellpyProducer.Produce(KafkaSettings.DefaultMessageCount, KafkaSettings.ClothingAdAvroTopic);
    }
}