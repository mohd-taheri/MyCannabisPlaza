using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.Infrastructure.Data;
using MongoDB.Bson;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoRepository<Order> _orderRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IMongoRepository<Basket> _basketRepository;
        private readonly IMongoRepository<Product> _itemRepository;

        public OrderService(IMongoRepository<Basket> basketRepository,
            IMongoRepository<Product> itemRepository,
            IMongoRepository<Order> orderRepository,
            IUriComposer uriComposer)
        {
            _orderRepository = orderRepository;
            _uriComposer = uriComposer;
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }

        public async Task CreateOrderAsync(string basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.FindOneAsync(x => x.Id == ObjectId.Parse(basketId));
            //Guard.Against.NullBasket(basketId, basket);
            var items = new List<ItemOrdered>();
            foreach (var item in basket.Items)
            {
                //var catalogItem = _itemRepository.FindOne(x => x.SecondaryId == item.SecondaryId);
                var itemOrdered = new ItemOrdered(item.StoreId,item.SecondaryId, item.Name, _uriComposer.ComposePicUri(item.PictureUri),item.Price,item.Amount);
                //var orderItem = new OrderItem(itemOrdered, item.Price, item.Amount);
                items.Add(itemOrdered);
            }
            var order = new Order(basket.BuyerId, basket.StoreId, shippingAddress, items);

            await _orderRepository.InsertOneAsync(order);
        }
    }
}
