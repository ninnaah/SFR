namespace Consumer.Database.Entities;

public class ClothingAd
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; }
    public string? Condition { get; set; }
    public string? Size { get; set; }
    public IList<string> Color { get; set; }
    public IList<string> Material { get; set; }
    public double Price { get; set; }
    public string Currency { get; set; }
    public string? Location { get; set; }
    public string? SellerId { get; set; }
    public IList<string> PhotoUrls { get; set; }
    public string? PublishedAt { get; set; }
    public string Source { get; set; }
    
    public ClothingAd()
    {
        Color = new List<string>();
        Material = new List<string>();
        PhotoUrls = new List<string>();
    }
}