using Shared.AvroModels.V1;

namespace Producer.Models.Mapper
{
    public static class WillhabenItemMapper
    {
        public static ClothingAdAvro ToClothingAdAvroV1(WillhabenItem item)
        {
            if (item == null) return null;

            return new ClothingAdAvro
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Category = item.Category,
                Condition = item.Condition,
                Size = "Unbekannt", // Kein Feld in Willhaben → default
                Color = new List<string> { "Unbekannt" },
                Material = new List<string> { "Unbekannt" },
                Price = double.TryParse(item.Price, out var parsedPrice) ? parsedPrice : 0.0,
                Currency = item.Currency,
                Location = item.Address2 ?? item.Location,
                SellerId = item.SellerId,
                PhotoUrls = item.PhotoUrls ?? new List<string>(),
                PublishedAt = item.PublishedAt.ToString("o"),
                Source = "Willhaben"
            };
        }
    }
}