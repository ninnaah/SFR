using SFR.Models;

    public static class SellpyItemToClothingAdMapper
    {
        public static ClothingAd Map(SellpyItem sellpyItem)
        {
            if (sellpyItem == null)
            {
                return null;
            }

            return new ClothingAd(
                id: sellpyItem.ObjectId,
                title: sellpyItem.Headline,
                description: sellpyItem.Description,
                price: sellpyItem.Price,
                currency: sellpyItem.Currency ?? "EUR",
                category: sellpyItem.Category,
                size: sellpyItem.Size,
                color: sellpyItem.Colors ?? new List<string>(),
                material: sellpyItem.Materials ?? new List<string>(),
                condition: sellpyItem.Condition,
                sellerId: sellpyItem.SellerId,
                location: sellpyItem.Location,
                photoUrls: sellpyItem.PhotoUrls ?? new List<string>(),
                publishedAt: sellpyItem.CreatedAt,
                source: "Sellpy"
            );
        }
    }
