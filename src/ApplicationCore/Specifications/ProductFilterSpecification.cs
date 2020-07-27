using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class ProductFilterSpecification : BaseSpecification<Product>
    {
        
        public ProductFilterSpecification(string name)        
            : base(i => (!string.IsNullOrEmpty(name) || i.Name == name))
        {
        }
    }
}
