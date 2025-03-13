namespace SFR.Models;

public class ClothingAd
{
    public ClothingAd(string name, double cost, string description)
    {
            Name = name;
            Cost = cost;
            Description = description;
    }
    public string Name { get; set; }
    public double Cost { get; set; }
    public string Description { get; set; }
}