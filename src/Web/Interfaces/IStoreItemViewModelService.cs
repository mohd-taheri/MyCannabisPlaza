using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public interface IStoreItemViewModelService
    {
        Task UpdateStoreItem(StoreItemViewModel viewModel);
    }
}
