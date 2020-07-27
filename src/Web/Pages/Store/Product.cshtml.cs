using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Store
{
    public class ProductModel : PageModel
    {
        private readonly IProductService _productService;
        private string _storeId;

        //[BindProperty(SupportsGet = true)]
        //public int Id { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string BrandFilterApplied { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string CategoryFilterApplied { get; set; }

        public ProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public StoreProductViewModel PrdModel { get; set; } = new StoreProductViewModel();
        public void OnGet(StoreProductViewModel storeModel,int storeId, int? pageId)
        {
            //SetStoreModelAsync(Id);
            PrdModel = _productService.GetStoreProducts(storeId, pageId ?? 0, Constants.ITEMS_PER_PAGE, storeModel.BrandFilterApplied, storeModel.CategoryFilterApplied, storeModel.SearchText);
        }

        //public void onPost(StoreProductViewModel storeModel, int storeId, int? pageId)
        //{
            //SetStoreModelAsync(Id);
            //PrdModel = _productService.GetStoreProducts(storeId, pageId ?? 0, Constants.ITEMS_PER_PAGE, storeModel.BrandFilterApplied, storeModel.CategoryFilterApplied);
        //}

        private void SetStoreModelAsync(int storeId)
        {
            //if (_storeId != "0" || !String.IsNullOrEmpty(_storeId))
            //{
                GetOrSetStoreCookie(storeId);
            //}           
        }

        private void GetOrSetStoreCookie(int storeId)
        {
            if (Request.Cookies.ContainsKey(Constants.STORE_COOKIENAME))
            {
                _storeId = Request.Cookies[Constants.STORE_COOKIENAME];
            }
            if (_storeId != null) return;

            _storeId = storeId.ToString();
            var cookieOptions = new CookieOptions { IsEssential = true };
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.STORE_COOKIENAME, storeId.ToString(), cookieOptions);
        }
    }
}