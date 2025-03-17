namespace SFR.AvroModels.V1
{
    public class ClothingAdAvro
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string Size { get; set; }
        public List<string> Color { get; set; }
        public List<string> Material { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Location { get; set; }
        public string SellerId { get; set; }
        public List<string> PhotoUrls { get; set; }
        public string PublishedAt { get; set; }
        public string Source { get; set; }
    }
}