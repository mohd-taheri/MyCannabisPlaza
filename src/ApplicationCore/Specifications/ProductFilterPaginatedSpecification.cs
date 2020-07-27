using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class ProductFilterPaginatedSpecification : BaseSpecification<Product>
    {
        public ProductFilterPaginatedSpecification(int skip, int take, string name)
            : base(i => (!string.IsNullOrEmpty(name) || i.Name == name))
        {
            ApplyPaging(skip, take);
        }
    }
}
