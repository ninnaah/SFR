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
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString));
            })
            .Build();
        
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

        using (var scope = host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var consumer = new MultiTopicConsumer(dbContext);
            consumer.Consume();
        }
        
    }
}