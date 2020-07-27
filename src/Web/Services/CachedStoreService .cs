using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.eShopWeb.Web.Extensions;
using System;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using MongoDB.Bson;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CachedStoreService : IStoreService
    {
        private readonly IMemoryCache _cache;
        private readonly StoreService _storeService;        
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);
        public CachedStoreService(IMemoryCache cache,
        StoreService storeService)
        {
            _cache = cache;
            _storeService = storeService;
        }

        public StoreIndexViewModel GetStoreItems(int pageIndex, int itemsPage)
        {
            string cacheKey = $"stores-{pageIndex}-{itemsPage}";
            return _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return _storeService.GetStoreItems(pageIndex, itemsPage);
            });
        }

        public StoreItemViewModel Get(ObjectId id)
        {
            string cacheKey = $"store-{id}";
            return _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return _storeService.Get(id);
            });
        }

        public List<SelectListItem> GetStoreCities(int id)
        {
            string cacheKey = $"store-{id}-CityList";
            return _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                return _storeService.GetStoreCities(id);
            });
        }
    }
}
