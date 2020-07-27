using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class StoreProductViewModel
    {
        public int StoreId { get; set; }
        public IEnumerable<StoreProductItemViewModel> StoreProducts { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public string BrandFilterApplied { get; set; }
        public string CategoryFilterApplied { get; set; }
        public string SearchText { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
