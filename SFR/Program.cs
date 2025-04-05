using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SFR.Infrastructure.Consumer;
using SFR.Infrastructure.Producer;
using SFR.Services.Implementations;
using SFR.Configuration;
using SFR.Infrastructure.Database;

namespace SFR;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Bitte 'producer' oder 'consumer' als Argument angeben.");
            return;
        }

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString));
            })
            .Build();

        switch (args[0].ToLower())
        {
            case "producer":
                await RunProducers();
                break;
            case "consumer":
                RunConsumer(host);
                break;
            default:
                Console.WriteLine("Unbekannter Modus: " + args[0]);
                break;
        }
    }

    private static async Task RunProducers()
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

    private static void RunConsumer(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var consumer = new MultiTopicConsumer(dbContext);
        consumer.Consume();
    }
}