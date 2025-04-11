using Producer.Models;

namespace Producer.Services;

public interface IWillhabenItemService
{
    WillhabenItem GetRandomItem(); 
    List<WillhabenItem> GetRandomItems(int count);
}