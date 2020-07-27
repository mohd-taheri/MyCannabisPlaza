using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Basket
{
    public class IndexModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private string _username = null;
        private readonly IBasketViewModelService _basketViewModelService;
        private readonly IStoreService _storeService;
        private readonly IOrderService _orderService;
        private int _storeId = 0;

        public IndexModel(IBasketService basketService,
            IBasketViewModelService basketViewModelService,
            SignInManager<ApplicationUser> signInManager,
            IStoreService storeService,
            IOrderService orderServcie
           )
        {
            _basketService = basketService;
            _signInManager = signInManager;
            _basketViewModelService = basketViewModelService;
            _storeService = storeService;
            _orderService = orderServcie;
        }

        public BasketViewModel BasketModel { get; set; } = new BasketViewModel();
        public List<SelectListItem> CityList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public CheckoutViewModel AddressInfo { get; set; }

        public async Task OnGet(int Id)
        {
            await SetBasketModelAsync();
        }

        public async Task<IActionResult> OnPost(StoreProductItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToPage("/Index");
            }
            await SetBasketModelAsync();

            await _basketService.AddItemToBasket(BasketModel.Id, productDetails.StoreId, productDetails.SecondaryId, productDetails.ProductId, productDetails.OptionId, productDetails.Name, productDetails.PictureUri, productDetails.Price);

            await SetBasketModelAsync();


            return RedirectToPage();
        }

        public async Task OnPostUpdate(Dictionary<string, int> items)
        {
            await SetBasketModelAsync();
            await _basketService.SetQuantities(BasketModel.Id, items);
            await SetBasketModelAsync();
        }

        public async Task OnPostCheckoutProceed(CheckoutViewModel AddressInfo)
        {
            await SetBasketModelAsync();

            //await _basketService.SetQuantities(BasketModel.Id, checkoutViewModel);


            await _orderService.CreateOrderAsync(BasketModel.Id,
                new Address
                (AddressInfo.State, AddressInfo.City, AddressInfo.Address, AddressInfo.ZipCode, 
                AddressInfo.PhoneNumber,
                ImageToBytes(AddressInfo.IDCard), AddressInfo.Email,
                AddressInfo.Birthdate, AddressInfo.Agreement
                )
                );

            await _basketService.DeleteBasketAsync(BasketModel.Id);

            //return RedirectToPage();

        }

        private byte[] ImageToBytes(IFormFile img)
        {
            byte[] fileBytes;
            //if (img.Length > 0)
            //{
            using (var ms = new MemoryStream())
            {
                img.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            //}
            return fileBytes;
        }


        public async Task OnGetDeleteItem(string id)
        {
            await SetBasketModelAsync();
            await _basketService.RemoveItemFromBasket(BasketModel.Id, id);
            await SetBasketModelAsync();
        }

        public async Task OnPostDelete()
        {
            await SetBasketModelAsync();
            await _basketService.DeleteBasketAsync(BasketModel.Id);
            await SetBasketModelAsync();
        }

        private async Task SetBasketModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name, _storeId);
            }
            else
            {
                GetOrSetBasketCookieAndUserName();
                BasketModel = await _basketViewModelService.GetOrCreateBasketForUser(_username, _storeId);                
            }
            CityList = _storeService.GetStoreCities(BasketModel.Items[0].StoreId);
        }

        private void GetOrSetBasketCookieAndUserName()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                _username = Request.Cookies[Constants.BASKET_COOKIENAME];
            }

            // if (Request.Cookies.ContainsKey(Constants.STORE_COOKIENAME))
            // {
            //     _storeId = Request.Cookies[Constants.STORE_COOKIENAME];
            // }
            if (_username != null) return;

            _username = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true };
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, _username, cookieOptions);
        }
    }
}
