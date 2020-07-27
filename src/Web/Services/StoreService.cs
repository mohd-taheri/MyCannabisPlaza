using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class StoreService : IStoreService
    {        
        private readonly IMongoRepository<Store> _storeRepository;
        private readonly ILogger<Store> _logger;        
        private readonly IUriComposer _uriComposer;

        public StoreService(
            ILoggerFactory loggerFactory,                                 
            IUriComposer uriComposer,
            IMongoRepository<Store> storeRepository)
        {
            _logger = loggerFactory.CreateLogger<Store>();            
            _uriComposer = uriComposer;
            _storeRepository = storeRepository;
        }

        public StoreIndexViewModel GetStoreItems(int pageIndex, int itemsPage)
        {
            var itemsOnPage = _storeRepository.FilterBy(x => true);
            _logger.LogInformation("GetStoreItens called.");

            var filterSpecification = new StoreFilterSpecification(string.Empty);
            var filterPaginatedSpecification =
                new StoreFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, string.Empty);

            var vm = new StoreIndexViewModel()
            {
                StoreItems = itemsOnPage.Select(i => new StoreItemViewModel()
                {                
                    Id= i.Id,
                    SecondaryId = i.SecondaryId,
                    Name = i.Name,
                    Description = i.Description,
                    City= i.City,
                    PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                    Phone = i.Phone,
                    ZipCode = i.ZipCode,
                    Address = i.Address,
                    State = i.State,
                    License = i.License,
                    DeliveryZipCode = i.DeliveryZipCode
                }),
                //Brands = await GetBrands(),
                //Types = await GetTypes(),
                //BrandFilterApplied = brandId ?? 0,
                //TypesFilterApplied = typeId ?? 0,
                //PaginationInfo = new PaginationInfoViewModel()
                //{
                //    ActualPage = pageIndex,
                //    ItemsPerPage = itemsOnPage.Count,
                //    TotalItems = totalItems,
                //    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                //}
            };

            //vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            //vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        public StoreItemViewModel Get(ObjectId id) { 
           var dm  =  _storeRepository.FindOne(store => store.Id == id);
            var vm = new StoreItemViewModel()
                {
                Id = dm.Id,
                SecondaryId = dm.SecondaryId,
                Name = dm.Name,
                Description = dm.Description,
                City = dm.City,
                PictureUri = _uriComposer.ComposePicUri(dm.PictureUri),
                Phone = dm.Phone,
                ZipCode = dm.ZipCode,
                Address = dm.Address,
                State = dm.State,
                License = dm.License,
                DeliveryZipCode = dm.DeliveryZipCode
            };
           
            return vm;
        }
        public Store Create(Store store)
        {
            _storeRepository.InsertOne(store);
            return store;
        }

        public void Update(ObjectId id, Store storeIn) { } //=>
            //_storeRepository.ReplaceOne(store => store.Id == id, storeIn);

        public void Remove(Store storeIn) =>
            _storeRepository.DeleteOne(store => store.Id == storeIn.Id);

        public void Remove(ObjectId id) =>
            _storeRepository.DeleteOne(store => store.Id == id);

        public List<SelectListItem> GetStoreCities(int id)
        {
            var dm = _storeRepository.FindOne(store => store.SecondaryId == id).DeliveryZipCode;
            var vm = new List<SelectListItem>();
            foreach (var item in dm)
            {
                var i = new SelectListItem
                {
                    Value = item.City,
                    Text = item.City
                };

                vm.Add(i);
            }            
            return vm;
        }
    }
}
