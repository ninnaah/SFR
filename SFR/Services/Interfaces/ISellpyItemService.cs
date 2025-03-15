using SFR.Models;

namespace SFR.Services.Interfaces
{
    public interface ISellpyItemService
    {
        SellpyItem GetRandomItem(); 
        List<SellpyItem> GetRandomItems(int count);
    }
}