namespace SFR.Models
{
    // https://pro-docs.svc.vinted.com/#vinted-pro-integrations
    public class VintedItem
    {
        public int CatalogId { get; set; }
        public List<int> ColorIds { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public bool IsUnisex { get; set; }
        public int PackageSizeId { get; set; }
        public List<string> PhotoUrls { get; set; }
        public double Price { get; set; }
        public int SizeId { get; set; }
        public int StatusId { get; set; }
        public string Title { get; set; }
        public string ItemReference { get; set; }
        public bool IsDraft { get; set; }

        public VintedItem()
        {
            ColorIds = new List<int>();
            PhotoUrls = new List<string>();
        }
    }
}