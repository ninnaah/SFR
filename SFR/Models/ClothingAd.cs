namespace SFR.Models
{
    public class ClothingAd
    {
        public ClothingAd(
            string id,
            string title,
            string description,
            double price,
            string currency,
            string category,
            string size,
            List<string> color,
            List<string> material,
            string condition,
            string sellerId,
            string location,
            List<string> photoUrls,
            DateTime publishedAt,
            string source
        )
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Currency = currency;
            Category = category;
            Size = size;
            Color = color ?? new List<string>();
            Material = material ?? new List<string>();
            Condition = condition;
            SellerId = sellerId;
            Location = location;
            PhotoUrls = photoUrls ?? new List<string>();
            PublishedAt = publishedAt;
            Source = source;
        }

        public string Id { get; set; }               // Eindeutige ID
        public string Title { get; set; }            // Titel des Artikels
        public string Description { get; set; }      // Beschreibung
        public double Price { get; set; }            // Preis
        public string Currency { get; set; }         // Waehrung
        public string Category { get; set; }         // Kategorie
        public string Size { get; set; }             // Groesse
        public List<string> Color { get; set; }      // Farben
        public List<string> Material { get; set; }   // Materialien
        public string Condition { get; set; }        // Zustand
        public string SellerId { get; set; }         // Verkaeufer-ID
        public string Location { get; set; }         // Standort (z.B. Stadt)
        public List<string> PhotoUrls { get; set; }  // Bilder
        public DateTime PublishedAt { get; set; }    // Veroeffentlicht am
        public string Source { get; set; }           // Quelle (z.B. Willhaben, Vinted, Sellpy)
    }
}