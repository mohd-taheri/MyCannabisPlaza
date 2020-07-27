using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.eShopWeb.Web.Pages.Basket
{
    public class BasketViewModel
    {
        public string Id { get; set; }        
        public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
        public string BuyerId { get; set; }
        public int StoreId { get; set; }
        public int ItemsCount()
        {
            return Items.Sum(x=>x.Quantity);
        }
        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
        }
    }
}
