using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IMongoRepository<Basket> _basketRepository;        
        private readonly IUriComposer _uriComposer;
        private readonly IMongoRepository<Product> _producRepository;

        public BasketViewModelService(IMongoRepository<Basket> basketRepository,
            IMongoRepository<Product> producRepository,
            IUriComposer uriComposer)
        {
            _basketRepository = basketRepository;
            _uriComposer = uriComposer;
            _producRepository = producRepository;
        }

        //public BasketViewModel GetOrCreateBasketForUser(string userName,int storeId)
        //{
        //    //var basketSpec = new BasketWithItemsSpecification(userName);
        //    var basket = _basketRepository.FindOne(x=> x.BuyerId == userName);

        //    if (basket == null)
        //    {
        //        return CreateBasketForUser(userName,storeId);
        //    }
        //    return CreateViewModelFromBasket(basket);
        //}


        public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName, int storeId)
        {
            //var basketSpec = new BasketWithItemsSpecification(userName);
            var basket = _basketRepository.FindOne(x => x.BuyerId == userName);

            if (basket == null)
            {
                return await CreateBasketForUser(userName, storeId);
            }
            return await CreateViewModelFromBasket(basket);
        }

        private async Task<BasketViewModel> CreateViewModelFromBasket(Basket basket)
        {
            var viewModel = new BasketViewModel();
            viewModel.Id = basket.Id.ToString();
            viewModel.BuyerId = basket.BuyerId;
            viewModel.Items = await GetBasketItems(basket.Items); 
            return viewModel;
        }
        private async Task<BasketViewModel> CreateBasketForUser(string userId,int storeId)
        {
            var basket = new Basket(userId, storeId);
           await _basketRepository.InsertOneAsync(basket);

            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id.ToString(),
                Items = new List<BasketItemViewModel>()
            };
        }
        private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
        {
            var items = new List<BasketItemViewModel>();
            foreach (var item in basketItems)
            {
                var itemModel = new BasketItemViewModel
                {
                    //Id = item.Id.ToString(),
                    StoreId = item.StoreId,
                    SecondaryId = item.SecondaryId,
                    ProductName = item.Name,
                    UnitPrice = item.Price,
                    Quantity = item.Amount,
                    ProductId = item.ProductId,
                    OptionId = item.OptionId,
                    PictureUrl = _uriComposer.ComposePicUri(item.PictureUri)
            };
                //var catalogItem = _itemRepository.find(item.CatalogItemId);
                
                //itemModel.ProductName = catalogItem.Name;
                items.Add(itemModel);
            }

            return items;
        }
    }
}
