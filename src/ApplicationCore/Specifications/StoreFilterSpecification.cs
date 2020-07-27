using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class StoreFilterSpecification : BaseSpecification<StoreItem>
    {
        
        public StoreFilterSpecification(string city)        
            : base(i => (!string.IsNullOrEmpty(city) || i.City == city))
        {
        }
    }
}
