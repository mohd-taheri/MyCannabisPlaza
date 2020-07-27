using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public interface IStoreViewModelService
    {
        Task<StoreIndexViewModel> GetStoreItems(int pageIndex, int itemsPage);        
    }
}
