using SFR.Infrastructure;
using SFR.Infrastructure.Producer;
using SFR.Services.Implementations;

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

        await willhabenProducer.Produce(10, "willhaben-items");
        await vintedProducer.Produce(10, "vinted-items");
        await sellpyProducer.Produce(10, "sellpy-items");

        var consumer = new MultiTopicConsumer();
        consumer.Consume();
    }
}