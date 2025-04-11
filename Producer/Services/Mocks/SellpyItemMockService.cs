using Producer.Models;

namespace Producer.Services.Mocks
{
    public class SellpyItemMockService : ISellpyItemService
    {
        private static readonly Random Random = new Random();

        private readonly string[] Brands = { "Nike", "Adidas", "Zara", "H&M", "Levi's" };
        private readonly string[] Categories = { "Schuhe", "T-Shirts", "Jacken", "Hosen", "Socken" };
        private readonly string[] Conditions = { "Neu", "Sehr gut", "Gut", "Akzeptabel" };
        private readonly string[] Colors = { "Schwarz", "Weiss", "Blau", "Rot", "Gruen" };
        private readonly string[] Materials = { "Baumwolle", "Polyester", "Wolle", "Leinen" };

        public SellpyItem GetRandomItem()
        {
            return new SellpyItem
            {
                ObjectId = Guid.NewGuid().ToString(),
                Headline = $"{GetRandom(Brands)} {GetRandom(Categories)}",
                Description = "Top Second-Hand Artikel in bestem Zustand.",
                Brand = GetRandom(Brands),
                Size = GetRandom(new[] { "XS", "S", "M", "L", "XL" }),
                Condition = GetRandom(Conditions),
                Colors = new List<string> { GetRandom(Colors) },
                Materials = new List<string> { GetRandom(Materials) },
                Price = Math.Round(Random.NextDouble() * 100, 2),
                Currency = "EUR",
                SellerId = Guid.NewGuid().ToString(),
                Location = "Wien",
                Category = GetRandom(Categories),
                PhotoUrls = new List<string>
                {
                    $"https://example.com/photos/{Guid.NewGuid()}.jpg"
                },
                Weight = Math.Round(Random.NextDouble() * 2, 2),
                WarehouseId = $"WH-{Random.Next(1, 100)}",
                CreatedAt = DateTime.UtcNow.AddDays(-Random.Next(1, 30)),
                IsReturnable = Random.Next(0, 2) == 1
            };
        }

        public List<SellpyItem> GetRandomItems(int count)
        {
            var items = new List<SellpyItem>();
            for (int i = 0; i < count; i++)
            {
                items.Add(GetRandomItem());
            }

            return items;
        }

        private string GetRandom(string[] array)
        {
            return array[Random.Next(array.Length)];
        }
    }
}