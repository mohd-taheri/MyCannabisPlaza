using Ardalis.GuardClauses;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
{
    /// <summary>
    /// Represents a snapshot of the item that was ordered. If catalog item details change, details of
    /// the item that was part of a completed order should not change.
    /// </summary>
    public class ItemOrdered // ValueObject
    {
        public ItemOrdered(int storeId,string secondaryId, string productName, string pictureUri, decimal price, int amount=1)
        {
            //Guard.Against.OutOfRange(secondaryId, nameof(secondaryId), 1, int.MaxValue);
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));

            StoreId = storeId;
            SecondaryId = secondaryId;
            ProductName = productName;
            PictureUri = pictureUri;
            Price = price;
            Amount = amount;
        }

        private ItemOrdered()
        {
            // required by EF
        }

        public int StoreId { get; private set; }
        public string SecondaryId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
        public decimal Price { get; private set; }
        public int Amount { get; private set; }
    }
}
