using Producer.Models;

namespace Producer.Services
{
    public interface ISellpyItemService
    {
        SellpyItem GetRandomItem(); 
        List<SellpyItem> GetRandomItems(int count);
    }
}