using SFR.Models;

namespace SFR.Models.Mapper
{
    public static class VintedItemMapper
    {
        public static ClothingAd ToClothingAd(VintedItem item)
        {
            if (item == null) return null;

            return new ClothingAd(
                id: item.ItemReference,
                title: item.Title,
                description: item.Description,
                price: item.Price,
                currency: item.Currency,
                category: item.CatalogId.ToString(), // Du kannst hier eine Mapping-Logik einbauen
                size: item.SizeId.ToString(), // Evtl. Mapping zu Größe
                color: item.ColorIds.Select(id => id.ToString()).ToList(), // Evtl. Mapping zu Farben
                material: new List<string>(), // Vinted liefert hier keine Info
                condition: item.StatusId.ToString(), // Evtl. Mapping zu "Neu", "Gebraucht", ...
                sellerId: "Unbekannt", // Kein SellerId im VintedItem
                location: "Unbekannt", // Kein Standort im VintedItem
                photoUrls: item.PhotoUrls,
                publishedAt: DateTime.UtcNow, // Keine Info → Standardwert
                source: "Vinted"
            );
        }
    }
}