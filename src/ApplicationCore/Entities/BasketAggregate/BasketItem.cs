using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    [BsonIgnoreExtraElements]
    public class BasketItem
    {
        public int StoreId { get; set; }
        public string SecondaryId { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public BasketItem(int storeId, string secondaryId, int productId,int optionId,string name, string pictureUri,decimal price,int amount)
        {
            StoreId = storeId;
            SecondaryId = secondaryId;
            ProductId = productId;
            OptionId = optionId;
            Name = name;
            PictureUri = pictureUri;
            Price = price;
            Amount = amount;
        }
        public void AddQuantity(int amount)
        {
            Amount += amount;
        }

        public void SetNewQuantity(int amount)
        {
            Amount = amount;
        }
    }
}
