using Shared.AvroModels.V1;

namespace Producer.Models.Mapper
{
    public static class SellpyItemMapper
    {
        public static ClothingAdAvro ToClothingAdAvro(SellpyItem sellpyItem)
        {
            if (sellpyItem == null)
            {
                return null;
            }

            return new ClothingAdAvro
            {
                Id = sellpyItem.ObjectId,
                Title = sellpyItem.Headline,
                Description = sellpyItem.Description,
                Category = sellpyItem.Category ?? "Unbekannt",
                Condition = sellpyItem.Condition ?? "Unbekannt",
                Size = sellpyItem.Size ?? "Unbekannt",
                Color = sellpyItem.Colors ?? new List<string>(),
                Material = sellpyItem.Materials ?? new List<string>(),
                Price = sellpyItem.Price,
                Currency = sellpyItem.Currency ?? "EUR",
                Location = sellpyItem.Location ?? "Unbekannt",
                SellerId = sellpyItem.SellerId ?? "Unbekannt",
                PhotoUrls = sellpyItem.PhotoUrls ?? new List<string>(),
                PublishedAt = sellpyItem.CreatedAt.ToString("o"),
                Source = "Sellpy"
            };
        }
    }
}