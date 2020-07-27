using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public interface IProductService
    {
        StoreProductViewModel GetStoreProducts(int storeId, int pageIndex, int itemsPage, string brandName, string categoryName, string searchText);
    }
}
