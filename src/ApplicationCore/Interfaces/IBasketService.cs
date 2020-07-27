using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<int> GetBasketItemCountAsync(string userName);
        Task TransferBasketAsync(string anonymousId, string userName);
        Task AddItemToBasket(string basketId, int storeId, string secondaryId, int productId, int optionId, string name, string pictureUri, decimal unitPrice, int quantity = 1);
        Task SetQuantities(string basketId, Dictionary<string, int> quantities);
        Task DeleteBasketAsync(string basketId);
        Task RemoveItemFromBasket(string basketId,string secondaryId);
    }
}
