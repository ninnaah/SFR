using SFR.Models;
using SFR.Services.Interfaces;

namespace SFR.Services.Implementations
{
    public class WillhabenItemMockService : IWillhabenItemService
    {
        private static readonly Random Random = new Random();

        private readonly string[] Categories = { "Kleidung" }; // ML TODO: passt nur Kleidung?
        private readonly string[] Colors = { "Schwarz", "Weiss", "Blau", "Rot", "Gelb", "Gruen" };
        private readonly string[] Conditions = { "Neu", "Sehr gut", "Gut", "Akzeptabel" };
        private readonly string[] Genders = { "Herren", "Damen", "Unisex" };
        private readonly string[] Materials = { "Baumwolle", "Leinen", "Polyester", "Wolle" };
        private readonly string[] Sizes = { "XS", "S", "M", "L", "XL" };

        public WillhabenItem GetRandomItem()
        {
            var category = "Kleidung";
            var condition = GetRandomCondition();
            var size = GetRandomSize();
            var color = GetRandomColor();
            var material = GetRandomMaterial();
            var gender = GetRandomGender();

            var item = new WillhabenItem
            {
                Id = Guid.NewGuid().ToString(),
                Url = $"https://www.willhaben.at/iad/kaufen-und-verkaufen/d/{category}-{Guid.NewGuid().ToString().Substring(0, 8)}",
                Title = $"{category}: {gender} {size} - {color}",
                Description = $"Ein {condition}es Kleidungsstück aus {material} in der Größe {size}.",

                Price = (Random.Next(10, 150)).ToString(),
                PriceForDisplay = $"€ {Random.Next(10, 150)}",
                Currency = "EUR",

                Location = "Wien, 01. Bezirk, Innere Stadt",
                Address2 = "Wien",
                Address3 = "01. Bezirk",
                Address4 = "Wien",
                Coordinates = "48.2082,16.3738",
                ShowMap = true,

                PhotoUrls = new List<string>
                {
                    $"https://example.com/photos/{Guid.NewGuid()}.jpg"
                },

                Category = category,
                Condition = condition,

                Mileage = 0,
                PowerKw = 0,
                PowerPs = 0,
                FuelType = "Keine",
                Transmission = "Keine",

                NumberOfDoors = 0,
                NumberOfSeats = 0,

                SellerId = Guid.NewGuid().ToString(),
                SellerUuid = Guid.NewGuid().ToString(),
                PartnerId = $"mp_{Guid.NewGuid().ToString().Substring(0, 8)}",
                IsPrivate = true,
                IsDealer = false,
                SellerOrgType = "2",

                PublishedAt = DateTime.Now.AddDays(-Random.Next(1, 60)),
                UpdatedAt = DateTime.Now,

                IsDraft = false,
                P2PEnabled = true, // Immer PayLivery möglich bei Kleidung

                Highlights = new List<string>
                {
                    $"Groesse: {size}",
                    $"Farbe: {color}",
                    $"Material: {material}"
                }
            };

            return item;
        }

        private string GetRandomCondition()
        {
            return Conditions[Random.Next(Conditions.Length)];
        }

        private string GetRandomSize()
        {
            return Sizes[Random.Next(Sizes.Length)];
        }

        private string GetRandomColor()
        {
            return Colors[Random.Next(Colors.Length)];
        }

        private string GetRandomMaterial()
        {
            return Materials[Random.Next(Materials.Length)];
        }

        private string GetRandomGender()
        {
            return Genders[Random.Next(Genders.Length)];
        }

        public List<WillhabenItem> GetRandomItems(int count)
        {
            var items = new List<WillhabenItem>();
            for (int i = 0; i < count; i++)
            {
                items.Add(GetRandomItem());
            }

            return items;
        }
    }
    }