using Shared.AvroModels.V1;

namespace Producer.Models.Mapper
{
    public static class VintedItemMapper
    {
        public static ClothingAdAvro ToClothingAdAvro(VintedItem item)
        {
            if (item == null) return null;

            return new ClothingAdAvro
            {
                Id = item.ItemReference,
                Title = item.Title,
                Description = item.Description,
                Category = item.CatalogId.ToString(), // Optional: Mapping Katalog-ID zu Kategorie-Name
                Condition = item.StatusId.ToString(), // Optional: Mapping Status-ID zu Beschreibung
                Size = item.SizeId.ToString(), // Optional: Mapping Größe
                Color = item.ColorIds?.Select(id => id.ToString()).ToList() ?? new List<string>(),
                Material = new List<string>(), // Keine Materialdaten vorhanden
                Price = item.Price,
                Currency = item.Currency ?? "EUR",
                Location = "Unbekannt", // Keine Location in VintedItem
                SellerId = "Unbekannt", // Keine SellerId in VintedItem
                PhotoUrls = item.PhotoUrls ?? new List<string>(),
                PublishedAt = DateTime.UtcNow.ToString("o"), // Kein Datum → aktuelles Datum
                Source = "Vinted"
            };
        }
    }
}