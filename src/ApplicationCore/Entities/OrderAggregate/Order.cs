using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
{
    [BsonIgnoreExtraElements]
    [BsonCollection("orders")]
    public class Order : BaseDocument, IAggregateRoot
    {
        private Order()
        {
            // required by EF
        }

        public Order(string buyerId,int storeId,Address shipToAddress, List<ItemOrdered> items)
        {
            Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
            Guard.Against.Null(shipToAddress, nameof(shipToAddress));
            Guard.Against.Null(items, nameof(items));

            BuyerId = buyerId;
            StoreId = storeId;
            ShipToAddress = shipToAddress;
            _orderItems = items;
        }
        public int StoreId { get; private set; }
        public string BuyerId { get; private set; }

        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        [BsonElement("Address")]
        public Address ShipToAddress { get; private set; }

        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method Order.AddOrderItem() which includes behavior.
        [BsonElement("OrderItems")]
        private readonly List<ItemOrdered> _orderItems = new List<ItemOrdered>();

        // Using List<>.AsReadOnly() 
        // This will create a read only wrapper around the private list so is protected against "external updates".
        // It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
        //https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx 
        public IReadOnlyCollection<ItemOrdered> OrderItems => _orderItems.AsReadOnly();

        [BsonElement("TotalPrice")]
        public decimal TotalPrice => CalculateTotalPrice();

        [BsonElement("TotalAmount")]
        public decimal TotalAmount => CalculateTotalAmount();

        public decimal CalculateTotalPrice()
        {
            var total = 0m;
            foreach (var item in _orderItems)
            {
                total += item.Price * item.Amount;
            }
            return total;
        }

        public decimal CalculateTotalAmount()
        {
            var total = 0m;
            foreach (var item in _orderItems)
            {
                total += item.Amount;
            }
            return total;
        }
    }
}
