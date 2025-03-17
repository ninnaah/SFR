namespace SFR.Models
{
    // ML TODO: @Nina deine ClothingAd ist jetzt ein ClothingAdAvro geworden
    public class ClothingAd
    {
        public ClothingAd() {}

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