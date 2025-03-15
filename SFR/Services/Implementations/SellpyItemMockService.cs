using System;
using System.Collections.Generic;
using SFR.Models;
using SFR.Services.Interfaces;

namespace SFR.Services.Implementations
{
    public class SellpyItemMockService : ISellpyItemService
    {
        private static readonly Random Random = new Random();

        private readonly string[] Brands = { "Nike", "Adidas", "Zara", "H&M", "Levi's" };
        private readonly string[] Types = { "T-Shirt", "Hose", "Jacke", "Schuhe", "Bluse" };
        private readonly string[] Conditions = { "Neu", "Sehr gut", "Gut", "Akzeptabel" };
        private readonly string[] Colors = { "Schwarz", "Weiss", "Blau", "Rot", "Gruen" };
        private readonly string[] Materials = { "Baumwolle", "Polyester", "Wolle", "Leinen" };
        private readonly string[] Patterns = { "Uni", "Gestreift", "Gepunktet", "Print" };
        private readonly string[] Necklines = { "Rundhals", "V-Ausschnitt" };
        private readonly string[] SleeveLengths = { "Kurzarm", "Langarm" };

        public SellpyItem GetRandomItem()
        {
            return new SellpyItem
            {
                CreatedAt = DateTime.Now.AddDays(-Random.Next(1, 100)),
                ObjectId = Guid.NewGuid().ToString(),
                ItemStatus = "utlagd",
                User = new User
                {
                    ObjectId = Guid.NewGuid().ToString()
                },
                Metadata = GetRandomMetadata(),
                MetadataTranslations = new Dictionary<string, Metadata>
                {
                    { "de", GetRandomMetadata() },
                    { "en", GetRandomMetadata() }
                },
                Photos = GetRandomPhotos(),
                ItemIO = GetRandomItemIO(),
                Images = GetRandomImageUrls(),
                ItemAbTestFraction = Math.Round(Random.NextDouble(), 2),
                Headline = "Top Sellpy Artikel",
                BodyText = "Ein super Produkt aus zweiter Hand.",
                SourceLanguage = "de",
                MaterialCompositions = GetRandomMaterialCompositions(),
                ShelfId = $"A-{Random.Next(1, 10)}-{Random.Next(1, 100)}",
                SalesChannel = "market",
                Weight = Math.Round(Random.NextDouble() * 2, 2), // kg
                Bag = new Bag
                {
                    ObjectId = Guid.NewGuid().ToString()
                }
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

        private Metadata GetRandomMetadata()
        {
            return new Metadata
            {
                Brand = GetRandom(Brands),
                Demography = GetRandom(new[] { "Herren", "Damen", "Unisex" }),
                Size = GetRandom(new[] { "XS", "S", "M", "L", "XL" }),
                Color = new List<string> { GetRandom(Colors) },
                Type = GetRandom(Types),
                Condition = GetRandom(Conditions),
                Material = new List<string> { GetRandom(Materials) },
                Pattern = GetRandom(Patterns),
                Neckline = GetRandom(Necklines),
                SleeveLength = GetRandom(SleeveLengths)
            };
        }

        private List<Photo> GetRandomPhotos()
        {
            var photos = new List<Photo>();
            for (int i = 0; i < 2; i++)
            {
                photos.Add(new Photo
                {
                    Value = new PhotoValue
                    {
                        Url = $"https://example.com/photos/{Guid.NewGuid()}.jpg",
                        Type = GetRandom(new[] { "robot", "brand", "material" }),
                        PhotographedAt = DateTime.Now.AddDays(-Random.Next(1, 30)),
                        PhotographedBy = Guid.NewGuid().ToString(),
                        PhotographedIn = $"Studio-{Random.Next(1, 10)}",
                        Tags = new List<string> { "size", "brand" }
                    }
                });
            }

            return photos;
        }

        private ItemIO GetRandomItemIO()
        {
            return new ItemIO
            {
                BodyOutputOrder = new List<string> { "brand", "type", "size", "color" },
                TitleOutputOrder = new List<string> { "brand", "type" }
            };
        }

        private List<string> GetRandomImageUrls()
        {
            return new List<string>
            {
                $"https://example.com/image/{Guid.NewGuid()}.jpg",
                $"https://example.com/image/{Guid.NewGuid()}.jpg"
            };
        }

        private List<MaterialComposition> GetRandomMaterialCompositions()
        {
            return new List<MaterialComposition>
            {
                new MaterialComposition
                {
                    Value = new CompositionValue
                    {
                        Composition = new List<Composition>
                        {
                            new Composition
                            {
                                Material = GetRandom(Materials),
                                Percent = 100
                            }
                        }
                    }
                }
            };
        }

        private string GetRandom(string[] array)
        {
            return array[Random.Next(array.Length)];
        }
    }
}