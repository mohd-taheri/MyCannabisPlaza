using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CachedProductService : IProductService
    {
        private readonly IMemoryCache _cache;
        private readonly ProductService _productService;
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedProductService(IMemoryCache cache,
       ProductService productService)
        {
            _cache = cache;
            _productService = productService;
        }
        public StoreProductViewModel GetStoreProducts(int storeId, int pageIndex, int itemsPage, string brandName, string categoryName, string searchText)
        {
            string cacheKey = $"storeproducts-{storeId}-{pageIndex}-{itemsPage}-{brandName}-{categoryName}-{searchText}";
            return _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return _productService.GetStoreProducts(storeId,pageIndex, itemsPage, brandName, categoryName,searchText);
            });
        }
    }
}
