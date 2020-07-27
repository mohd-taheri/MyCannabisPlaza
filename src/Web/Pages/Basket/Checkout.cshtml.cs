using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Basket
{
    public class CheckoutModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOrderService _orderService;
        private string _username = null;
        private readonly IBasketViewModelService _basketViewModelService;
        private int _storeId = 0;

        public CheckoutModel(IBasketService basketService,
            IBasketViewModelService basketViewModelService,
            SignInManager<ApplicationUser> signInManager,
            IOrderService orderService)
        {
            _basketService = basketService;
            _signInManager = signInManager;
            _orderService = orderService;
            _basketViewModelService = basketViewModelService;
        }

        public BasketViewModel BasketModel { get; set; } = new BasketViewModel();        

        public void OnGet()
        {
            //if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME) && _signInManager.IsSignedIn(HttpContext.User)){
                //BasketModel = _basketViewModelService.GetOrCreateBasketForUser(_username, _storeId);
            //    _basketService.TransferBasketAsync(Request.Cookies[Constants.BASKET_COOKIENAME], User.Identity.Name);
            //    RedirectToPage("basket");
            //}
        }

        public async Task<IActionResult> OnPost(CheckoutViewModel AddressInfo)
        {
            await SetBasketModelAsync();

            //await _basketService.SetQuantities(BasketModel.Id, checkoutViewModel);
            

            await _orderService.CreateOrderAsync(BasketModel.Id, 
                new Address
                (AddressInfo.State, AddressInfo.City, AddressInfo.Address, AddressInfo.ZipCode, AddressInfo.PhoneNumber, 
                ImageToBytes(AddressInfo.IDCard), AddressInfo.Email,
                AddressInfo.Birthdate, AddressInfo.Agreement                  
                )
                );

            await _basketService.DeleteBasketAsync(BasketModel.Id);

            return RedirectToPage();
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
        }

        private void GetOrSetBasketCookieAndUserName()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                _username = Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            if (_username != null) return;

            _username = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, _username, cookieOptions);
        }
    }
}
