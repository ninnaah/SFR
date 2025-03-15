namespace SFR.Models
{
    // https://pro-docs.svc.vinted.com/#vinted-pro-integrations
    public class VintedItem
    {
        public int CatalogId { get; set; }          // Katalog (z.B. Kategorie)
        public List<int> ColorIds { get; set; }     // Farben als ID-List
        public string Currency { get; set; }        // EUR etc.
        public string Description { get; set; }     // Artikelbeschreibung
        public bool IsUnisex { get; set; }          // Unisex true/false
        public int PackageSizeId { get; set; }      // ID für Paketgröße
        public List<string> PhotoUrls { get; set; } // Bilder URLs
        public double Price { get; set; }           // Preis als double
        public int SizeId { get; set; }             // ID für Größe
        public int StatusId { get; set; }           // Status (z.B. Neu, Gebraucht)
        public string Title { get; set; }           // Titelanzeige
        public string ItemReference { get; set; }   // Item Referenz ID (z.B. von dir intern)
        public bool IsDraft { get; set; }           // Entwurf true/false

        public VintedItem()
        {
            ColorIds = new List<int>();
            PhotoUrls = new List<string>();
        }
    }
}