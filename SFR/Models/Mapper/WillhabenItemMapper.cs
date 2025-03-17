namespace SFR.Models.Mapper
{
    public static class WillhabenItemMapper
    {
        public static ClothingAd ToClothingAd(WillhabenItem item)
        {
            if (item == null) return null;

            return new ClothingAd(
                id: item.Id,
                title: item.Title,
                description: item.Description,
                price: double.TryParse(item.Price, out var parsedPrice) ? parsedPrice : 0.0,
                currency: item.Currency,
                category: item.Category,
                size: "Unbekannt", // WillhabenItem hat kein direktes Size-Feld für Kleidung
                color: new List<string> { "Unbekannt" }, // Hier fehlt Farbe im Item → Platzhalter
                material: new List<string> { "Unbekannt" }, // Hier fehlt Material im Item → Platzhalter
                condition: item.Condition,
                sellerId: item.SellerId,
                location: item.Address2 ?? item.Location,
                photoUrls: item.PhotoUrls,
                publishedAt: item.PublishedAt,
                source: "Willhaben"
            );
        }
    }
}