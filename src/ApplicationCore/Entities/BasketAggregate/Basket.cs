using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    [BsonIgnoreExtraElements]
    [BsonCollection("baskets")]        
    public class Basket : BaseDocument, IAggregateRoot
    {
        public string BuyerId { get; private set; }
        public int StoreId { get; set; }
        [BsonElement("items")]
        private List<BasketItem> _items { get; set; }
        //[BsonElement("items")]
        //public IReadOnlyCollection<BasketItem> Items =>  _items.AsReadOnly();
        
        //[BsonElement("items")]
        public IReadOnlyCollection<BasketItem> Items 
        {
            get
            { 
                if ( _items == null){
                    return new List<BasketItem>();
                }
                else
                {
                    return _items.AsReadOnly();
                }
            } 
        }
        
        public Basket(string buyerId,int storeId)
        {
            BuyerId = buyerId;
            StoreId = storeId;
        }

        public void AddItem(int storeId,string secondaryId, int productId,int optionId,string name,string pictureUri, decimal unitPrice, int quantity = 1)
        {
            if (!Items.Any(i => i.SecondaryId == secondaryId))
            {
                var newitem = new BasketItem(storeId, secondaryId, productId, optionId, name, pictureUri, unitPrice, quantity);
                if (_items == null) _items = new List<BasketItem>();               
                _items?.Add(newitem);
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.SecondaryId == secondaryId);
            existingItem.AddQuantity(quantity);
        }
        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.Amount == 0);
        }
        public void SetNewBuyerId(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void DeleteItem(string secondaryId)
        {
            var existingItem = Items.FirstOrDefault(i => i.SecondaryId == secondaryId);
            if (existingItem != null)
            {
                _items?.Remove(existingItem);
            }                      
            
        }
    }   
}
