namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
{

    public class OrderItem : BaseDocument
    {
        public ItemOrdered ItemOrdered { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Units { get; private set; }
        

        public OrderItem(ItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }
    }
}
