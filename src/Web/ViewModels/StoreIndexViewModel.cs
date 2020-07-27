using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class StoreIndexViewModel
    {
        public IEnumerable<StoreItemViewModel> StoreItems { get; set; }
        public int? TypesFilterApplied { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
    
}
