using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Ardalis.GuardClauses;
using System;
using MongoDB.Bson;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IMongoRepository<Basket> _basketRepository;
        private readonly IAppLogger<BasketService> _logger;

        public BasketService(IMongoRepository<Basket> basketRepository,
            IAppLogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }

        public async Task AddItemToBasket(string basketId, int storeId, string secondaryId, int productId, int optionId, string name,string pictureUri, decimal unitPrice, int quantity = 1)
        {
            var basket =  await _basketRepository.FindByIdAsync(basketId);
            basket.StoreId = storeId;
            basket.AddItem(storeId, secondaryId, productId, optionId, name, pictureUri,unitPrice, quantity);
            await _basketRepository.ReplaceOneAsync(basket);
            //Console.WriteLine(basket.ToBsonDocument());
        }       

        public async Task DeleteBasketAsync(string basketId)
        {
            var basket = _basketRepository.FindById(basketId);
            await _basketRepository.DeleteByIdAsync(basketId);
        }

        public async Task RemoveItemFromBasket(string basketId, string secondaryId)
        {
            Guard.Against.Null(secondaryId, nameof(secondaryId));
            var basket = _basketRepository.FindOne(x => x.Id == ObjectId.Parse(basketId));
            basket.DeleteItem(secondaryId);
            basket.RemoveEmptyItems();
            await _basketRepository.ReplaceOneAsync(basket);
        }


        public async Task<int> GetBasketItemCountAsync(string userName)
        {
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            //var basketSpec = new BasketWithItemsSpecification(userName);
            var basket = await _basketRepository.FindOneAsync(x => x.BuyerId == userName);
            if (basket == null)
            {
                _logger.LogInformation($"No basket found for {userName}");
                return 0;
            }
            int count = 0;
            foreach (BasketItem item in basket.Items) {
                count += item.Amount;
            };            
            _logger.LogInformation($"Basket for {userName} has {count} items.");
            
            return count;
        }

        public async Task SetQuantities(string basketId, Dictionary<string, int> quantities)
        {
            Guard.Against.Null(quantities, nameof(quantities));
            var basket = _basketRepository.FindOne(x => x.Id == ObjectId.Parse(basketId));
            //Guard.Against.NullBasket(basketId, basket);
            foreach (var item in basket.Items)
            {
                if (quantities.TryGetValue(item.SecondaryId.ToString(), out var quantity))
                {
                    if (_logger != null) _logger.LogInformation($"Updating quantity of item ID:{item.SecondaryId} to {quantity}.");
                    item.SetNewQuantity(quantity);
                }
            }
            basket.RemoveEmptyItems();
            await _basketRepository.ReplaceOneAsync(basket);
        }

        public async Task TransferBasketAsync(string anonymousId, string userName)
        {
            Guard.Against.NullOrEmpty(anonymousId, nameof(anonymousId));
            Guard.Against.NullOrEmpty(userName, nameof(userName));
            //var basketSpec = new BasketWithItemsSpecification(anonymousId);
            var basket = (await _basketRepository.FindOneAsync(x => x.BuyerId == anonymousId));
            if (basket == null) return;
            basket.SetNewBuyerId(userName);
            await _basketRepository.ReplaceOneAsync(basket);
        }
    }
}
