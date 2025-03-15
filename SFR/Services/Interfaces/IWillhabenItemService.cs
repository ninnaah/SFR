using SFR.Models;

namespace SFR.Services.Interfaces;

public interface IWillhabenItemService
{
    WillhabenItem GetRandomItem(); 
    List<WillhabenItem> GetRandomItems(int count);
}