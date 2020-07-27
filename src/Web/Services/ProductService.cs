using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using System.Linq;
using System;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using System.Collections.Generic;
using MailKit.Search;

namespace Microsoft.eShopWeb.Web.Services
{
    public class ProductService : IProductService
    {        
        private readonly IMongoRepository<Product> _productRepository;
        private readonly IMongoRepository<Brand> _brandRepository;
        private readonly IMongoRepository<Category> _categoryRepository;
        private readonly ILogger<Product> _logger;
        private readonly IUriComposer _uriComposer;

        public ProductService(
          ILoggerFactory loggerFactory,          
          IUriComposer uriComposer,
          IMongoRepository<Product> productRepository,
          IMongoRepository<Brand> brandRepository,
          IMongoRepository<Category> categoryRepository)
        {
            _logger = loggerFactory.CreateLogger<Product>();
            _uriComposer = uriComposer;            
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }
        public StoreProductViewModel GetStoreProducts(int storeId, int pageIndex, int itemsPage, string brandName="All", string categoryName="All",string searchText="")
        {            
            _logger.LogInformation("GetStoreProducts called.");
            
            if (String.IsNullOrEmpty(brandName)){
                brandName = "All";
            }

            if (String.IsNullOrEmpty(categoryName))
            {
                categoryName = "All";
            }

            if (String.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }

            // var filterSpecification = new ProductFilterSpecification(string.Empty);
            // var filterPaginatedSpecification =
            //      new ProductFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, string.Empty);


            //var itemsOnPage = _productRepository.FilterBy(p => p.StoreId == storeId);


            //var filterSpecification = new CatalogFilterSpecification(brandId, typeId);
            //var filterPaginatedSpecification =
            //    new CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, brandId, typeId);

            // the implementation below using ForEach and Count. We need a List.
            //var itemsOnPage = _productRepository.Find(filterPaginatedSpecification);
            var totalItems = _productRepository.Count( 
                x => x.StoreId == storeId & (x.Brand == brandName || brandName == "All") 
                & 
                (x.Category == categoryName || categoryName == "All")
                &
                (x.Name.ToLower().Contains(searchText.ToLower()) || searchText == "")
                );

            //var test = _productRepository.Find(x => x.StoreId == storeId,1,2);

            //var test1 = _productRepository.Find(x => x.StoreId == storeId, 2, 2);

            //var command1 = new BsonDocument { { "StoreId", storeId } };
            //var command2 = new BsonDocument { { "Brand", brandName } };

            //var filterBuilder = Builders<BsonDocument>.Filter;
            //var filter = filterBuilder.Eq("StoreId", storeId)
                             //& filterBuilder.Eq("Brand", brandName);


            var itemsOnPage = _productRepository.Find(
                x => x.StoreId == storeId & (x.Brand == brandName || brandName == "All")
                &
                (x.Category == categoryName || categoryName == "All")
                &
                (x.Name.ToLower().Contains(searchText.ToLower()) || searchText == "")
                , pageIndex, itemsPage);
            //var itemsOnPage = _productRepository.Find(filterBuilder, pageIndex, itemsPage);

            var brands = _brandRepository.FilterBy(x => x.StoreId == storeId);

            //var brandsList = brands.Select(i => new SelectListItem()
            //{
                //Text = i.Name,
                //Value = i.Name //i.SecondaryId.ToString()
            //});

            //brandsList.Add(new SelectListItem() { Text = "All", Value = "All" });

            //brandsList.OfType<object>().Concat(second);

            //var x = brandsList.ToList();

            var categories = _categoryRepository.FilterBy(x => x.StoreId == storeId);

            var vm = new StoreProductViewModel()
            {
                StoreId = storeId,
                BrandFilterApplied = brandName ?? "All",
                CategoryFilterApplied = categoryName ?? "All",
                SearchText = searchText ?? "",
                StoreProducts = itemsOnPage.Select(i => new StoreProductItemViewModel()
                {
                    Id = i.Id.ToString(),
                    SecondaryId = i.SecondaryId,
                    StoreId = i.StoreId,
                    ProductId = i.ProductId,
                    Name = i.Name,
                    PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                    Description = i.Description,
                    Price = i.Price,
                    Amount = i.Amount,
                    IsMedical = i.IsMedical,
                    OptionId = i.OptionId,
                    PercentCbd = i.PercentCbd,
                    PercentThc = i.PercentThc,
                    Slung = i.Slung,
                    StrainType = i.StrainType,
                    Brand = i.Brand,
                    Category = i.Category,
                }),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    StoreId = storeId,
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count(),
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                },
                Brands = GetBrands(storeId),
                Categories = GetCategories(storeId)
            };

            //vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            //vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        public IEnumerable<SelectListItem> GetBrands(int storeId)
        {
            _logger.LogInformation("GetBrands called.");
            var brands = _brandRepository.FilterBy(x=> x.StoreId == storeId);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (Brand brand in brands)
            {
                items.Add(new SelectListItem() { Value = brand.Name, Text = brand.Name });
            }

            return items;
        }

        public IEnumerable<SelectListItem> GetCategories(int storeId)
        {
            _logger.LogInformation("GetCategories called.");
            var categories = _categoryRepository.FilterBy(x => x.StoreId == storeId);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (Category category in categories)
            {
                items.Add(new SelectListItem() { Value = category.Name, Text = category.Name });
            }

            return items;
        }
    }
}
