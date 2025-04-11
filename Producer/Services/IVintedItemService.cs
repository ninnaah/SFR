using Producer.Models;

namespace Producer.Services
{
    public interface IVintedItemService
    {
        VintedItem GetRandomItem();
        List<VintedItem> GetRandomItems(int count); 
    }
}