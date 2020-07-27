using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    [BsonIgnoreExtraElements]
    [BsonCollection("products")]
    public class Product: BaseDocument
    {       
        public string SecondaryId { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }        
        public string Name { get; set; }
        public string Slung { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string PercentThc { get; set; }
        public string PercentCbd { get; set; }        
        public string Description { get; set; }
        public string PictureUri { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
        public string Category { get; set; }
        public string StrainType { get; set; }
        public bool IsMedical { get; set; }
        public bool IsActive { get; set; }
    }
}
