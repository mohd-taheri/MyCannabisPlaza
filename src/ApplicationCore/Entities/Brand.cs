using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    [BsonIgnoreExtraElements]
    [BsonCollection("brands")]
    public class Brand: BaseDocument
    {
        public int StoreId { get; set; }
        public int SecondaryId { get; set; }
        public string Name { get; set; }
        public string PicureUri { get; set; }
    }
}
