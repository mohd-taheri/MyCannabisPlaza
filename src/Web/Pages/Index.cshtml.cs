using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Store
{
    public class IndexModel : PageModel
    {
        //private readonly IStoreViewModelService _storeViewModelService;
        private readonly IStoreService _storeService;

        //public IndexModel(IStoreViewModelService storeViewModelService)
        //{
        //    _storeViewModelService = storeViewModelService;
        //}

        public IndexModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public StoreIndexViewModel StoreModel { get; set; } = new StoreIndexViewModel();        

        //public async Task OnGet(StoreIndexViewModel storeModel, int? pageId)
        //{
        //    StoreModel = await _storeViewModelService.GetStoreItems(pageId ?? 0, Constants.ITEMS_PER_PAGE);
        //}

        public void OnGet(StoreIndexViewModel storeModel, int? pageId)
        {
            StoreModel = _storeService.GetStoreItems(pageId ?? 0, Constants.ITEMS_PER_PAGE);
        }
    }
}