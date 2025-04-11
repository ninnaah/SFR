namespace Producer.Models
{
    public class SellpyItem
    {
        public string ObjectId { get; set; } // Eindeutige ID
        public string Headline { get; set; } // Titel
        public string Description { get; set; } // Beschreibung

        public string Brand { get; set; } // Marke
        public string Size { get; set; } // Groesse
        public string Condition { get; set; } // Zustand

        public List<string> Colors { get; set; } // Farben
        public List<string> Materials { get; set; } // Materialien

        public double Price { get; set; } // Preis
        public string Currency { get; set; } = "EUR"; // Waerung

        public string SellerId { get; set; } // Verkaeufer-ID
        public string Location { get; set; } // Standort
        public string Category { get; set; } // Kategorie (z.B. "Socken", "Schuhe")

        public List<string> PhotoUrls { get; set; } // Bilder

        // Exklusive Felder fuer Sellpy
        public double Weight { get; set; } // Gewicht des Artikels in KG
        public string WarehouseId { get; set; } // Lagerort-ID
        public DateTime CreatedAt { get; set; } // Erstellungsdatum
        public bool IsReturnable { get; set; } // Rueckgabe erlaubt?

        public SellpyItem()
        {
            Colors = new List<string>();
            Materials = new List<string>();
            PhotoUrls = new List<string>();
        }
    }
}