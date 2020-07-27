using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.Data;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{

    public interface IOrderRepository : IMongoRepository<Order>
    {
        Task<Order> GetByIdWithItemsAsync(int id);
    }
}
