
using Microsoft.eShopWeb.ApplicationCore.Entities;
using MongoDB.Bson;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public sealed class BasketWithItemsSpecification  : BaseSpecification<Basket>
    {
        public BasketWithItemsSpecification(ObjectId basketId)
            :base(b => b.Id == basketId)
        {
            AddInclude(b => b.Id);
        }
        public BasketWithItemsSpecification(string buyerId)
            :base(b => b.BuyerId == buyerId)
        {
            AddInclude(b => b.Id);
        }
    }
}
