using SFR.Models;
using SFR.Services.Interfaces;

namespace SFR.Services.Implementations
{
    public class VintedItemMockService : IVintedItemService
    {
        private static readonly Random Random = new Random();

        // Beispiel-Daten für IDs (passen zu deiner API-Doku)
        private readonly int[] CatalogIds = { 1, 2, 3, 4, 5 }; // Kategorien
        private readonly int[] ColorIds = { 10, 20, 30, 40, 50 }; // Farben
        private readonly int[] SizeIds = { 100, 101, 102, 103, 104 }; // Größen
        private readonly int[] StatusIds = { 1, 2, 3 }; // Status (1 = Neu, 2 = Sehr gut...)
        private readonly int[] PackageSizeIds = { 1, 2, 3 }; // Paketgrößen

        public VintedItem GetRandomItem()
        {
            return new VintedItem
            {
                CatalogId = GetRandomCatalogId(),
                ColorIds = GetRandomColorIds(),
                Currency = "EUR",
                Description = "Ein schönes Kleidungsstück von Vinted",
                IsUnisex = Random.Next(0, 2) == 1,
                PackageSizeId = GetRandomPackageSizeId(),
                PhotoUrls = new List<string>
                {
                    $"https://example.com/photos/{Guid.NewGuid()}.jpg"
                },
                Price = Math.Round(Random.NextDouble() * 100, 2), // z.B. 0-100 €
                SizeId = GetRandomSizeId(),
                StatusId = GetRandomStatusId(),
                Title = $"Vinted Item {Guid.NewGuid().ToString().Substring(0, 8)}",
                ItemReference = Guid.NewGuid().ToString(),
                IsDraft = Random.Next(0, 2) == 1
            };
        }

        public List<VintedItem> GetRandomItems(int count)
        {
            var items = new List<VintedItem>();
            for (int i = 0; i < count; i++)
            {
                items.Add(GetRandomItem());
            }

            return items;
        }

        private int GetRandomCatalogId() => CatalogIds[Random.Next(CatalogIds.Length)];
        private int GetRandomPackageSizeId() => PackageSizeIds[Random.Next(PackageSizeIds.Length)];
        private int GetRandomSizeId() => SizeIds[Random.Next(SizeIds.Length)];
        private int GetRandomStatusId() => StatusIds[Random.Next(StatusIds.Length)];

        private List<int> GetRandomColorIds()
        {
            var colorIds = new List<int>();
            var count = Random.Next(1, 3); // max 2 Farben pro Item
            for (int i = 0; i < count; i++)
            {
                colorIds.Add(ColorIds[Random.Next(ColorIds.Length)]);
            }
            return colorIds;
        }
    }
}