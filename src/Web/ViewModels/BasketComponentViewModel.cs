using Microsoft.eShopWeb.Web.Pages.Basket;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class BasketComponentViewModel
    {
        public int ItemsCount { get; set; }
        public decimal Total { get; set; }
        public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
    }
}
