using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class StoreFilterPaginatedSpecification : BaseSpecification<StoreItem>
    {
        public StoreFilterPaginatedSpecification(int skip, int take, string city)
            : base(i => (!string.IsNullOrEmpty(city) || i.City == city))
        {
            ApplyPaging(skip, take);
        }
    }
}
