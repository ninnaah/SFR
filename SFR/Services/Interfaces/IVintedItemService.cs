using SFR.Models;

namespace SFR.Services.Interfaces
{
    public interface IVintedItemService
    {
        VintedItem GetRandomItem();
        List<VintedItem> GetRandomItems(int count); 
    }
}