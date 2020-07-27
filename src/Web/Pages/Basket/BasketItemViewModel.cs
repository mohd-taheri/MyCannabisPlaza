namespace Microsoft.eShopWeb.Web.Pages.Basket
{
    public class BasketItemViewModel
    {
        public int StoreId { get; set; }
        public string SecondaryId { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}
