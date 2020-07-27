using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using MongoDB.Bson;

namespace Microsoft.eShopWeb.Web.Pages.Store
{
    public class DetailModel : PageModel
    {
        private readonly IStoreService _storeService;
        public StoreItemViewModel StoreModel { get; set; } = new StoreItemViewModel();
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public DetailModel(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public void OnGet()
        {
            StoreModel = _storeService.Get(ObjectId.Parse(Id));
        }
    }
}