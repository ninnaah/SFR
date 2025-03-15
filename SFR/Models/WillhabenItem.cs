namespace SFR.Models

{
    // https://github.com/CP02A/willhaben
    public class WillhabenItem
    {
        // Basisinformationen
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Preis
        public string Price { get; set; }
        public string PriceForDisplay { get; set; }
        public string Currency { get; set; }

        // Standortdaten
        public string Location { get; set; }
        public string Address2 { get; set; } // Ort
        public string Address3 { get; set; } // Bezirk
        public string Address4 { get; set; } // Bundesland
        public string Coordinates { get; set; }
        public bool ShowMap { get; set; }

        // Bilder
        public List<string> PhotoUrls { get; set; }

        // Kategorie-/Detailinformationen (je nach Art)
        public string Category { get; set; }
        public string Condition { get; set; }
        public int Mileage { get; set; }
        public int PowerKw { get; set; }
        public int PowerPs { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public int NumberOfDoors { get; set; }
        public int NumberOfSeats { get; set; }

        // Verkäuferinformationen
        public string SellerId { get; set; }
        public string SellerUuid { get; set; }
        public string PartnerId { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsDealer { get; set; }
        public string SellerOrgType { get; set; }

        // Zeitstempel
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Status / Optionen
        public bool IsDraft { get; set; } // Analog zu Vinted, kannst du optional nutzen
        public bool P2PEnabled { get; set; } // PayLivery Option

        // Highlights
        public List<string> Highlights { get; set; }

        // Konstruktor
        public WillhabenItem()
        {
            PhotoUrls = new List<string>();
            Highlights = new List<string>();
        }
    }
}