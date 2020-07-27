using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.ViewModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public interface IStoreService
    {
        StoreIndexViewModel GetStoreItems(int pageIndex, int itemsPage);

        StoreItemViewModel Get(ObjectId id);

        List<SelectListItem> GetStoreCities(int id);

        //Task<StoreItemViewModel> Get(int SecondaryId);
        //Task<StoreItemViewModel> Get(string name);

        //Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId);
        //Task<IEnumerable<SelectListItem>> GetBrands();
        // Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
