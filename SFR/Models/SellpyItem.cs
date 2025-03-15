namespace SFR.Models
{
    public class SellpyItem
    {
        public DateTime CreatedAt { get; set; } // Erstellungsdatum des Artikels
        public string ObjectId { get; set; } // Eindeutige ID des Artikels
        public string ItemStatus { get; set; } // Status, z.B. "utlagd" = veröffentlicht
        public User User { get; set; } // User, der den Artikel verkauft
        public Metadata Metadata { get; set; } // Basis-Metadaten, z.B. Marke, Größe
        public Dictionary<string, Metadata> MetadataTranslations { get; set; } // Metadaten in verschiedenen Sprachen
        public List<Photo> Photos { get; set; } // Alle Fotos inkl. Details
        public ItemIO ItemIO { get; set; } // Reihenfolge von Body und Title im UI
        public List<string> Images { get; set; } // Nur die Bild-URLs (ohne Details)
        public double ItemAbTestFraction { get; set; } // A/B-Test-Kennzahl
        public string Headline { get; set; } // Artikelüberschrift
        public string BodyText { get; set; } // Artikelbeschreibung (optional)
        public string SourceLanguage { get; set; } // Ursprungssprache
        public List<MaterialComposition> MaterialCompositions { get; set; } // Materialanteile (z.B. Baumwolle 100%)
        public string ShelfId { get; set; } // Lagerort im Warenlager
        public string SalesChannel { get; set; } // z.B. "market"
        public double Weight { get; set; } // Gewicht in kg
        public Bag Bag { get; set; } // Die Tasche, zu der das Item gehört

        public SellpyItem()
        {
            MetadataTranslations = new Dictionary<string, Metadata>();
            Photos = new List<Photo>();
            Images = new List<string>();
            MaterialCompositions = new List<MaterialComposition>();
        }
    }

    public class User
    {
        public string ObjectId { get; set; } // Verkäufer-ID
    }

    public class Metadata
    {
        public string Brand { get; set; } // Marke
        public string Demography { get; set; } // Zielgruppe (Men, Women...)
        public string Size { get; set; } // Größe (z.B. MEN-INT-M)
        public List<string> Color { get; set; } // Farben
        public string Type { get; set; } // Produkttyp (T-shirt...)
        public string Condition { get; set; } // Zustand (z.B. Gut)
        public List<string> Material { get; set; } // Materialien (z.B. Baumwolle)
        public string Pattern { get; set; } // Muster (z.B. Print)
        public string Neckline { get; set; } // Ausschnitt (z.B. Rundhals)
        public string SleeveLength { get; set; } // Ärmellänge (z.B. Kurzarm)
    }

    public class Photo
    {
        public PhotoValue Value { get; set; } // Bilddetails
    }

    public class PhotoValue
    {
        public string Url { get; set; } // Bild-URL
        public string Type { get; set; } // Bildtyp (robot, brand, etc.)
        public DateTime PhotographedAt { get; set; } // Zeitpunkt des Fotos
        public string PhotographedBy { get; set; } // Fotograf-ID
        public string PhotographedIn { get; set; } // Ort des Shootings
        public List<string> Tags { get; set; } // Tags (z.B. "size")
    }

    public class ItemIO
    {
        public List<string> BodyOutputOrder { get; set; } // Reihenfolge für Body-Infos
        public List<string> TitleOutputOrder { get; set; } // Reihenfolge für Title-Infos
    }

    public class MaterialComposition
    {
        public CompositionValue Value { get; set; } // Materialdetails
    }

    public class CompositionValue
    {
        public List<Composition> Composition { get; set; } // Liste der Anteile
    }

    public class Composition
    {
        public string Material { get; set; } // Materialname (z.B. Cotton)
        public int Percent { get; set; } // Prozentanteil
    }

    public class Bag
    {
        public string ObjectId { get; set; } // ID der Tasche im Lager
    }
}