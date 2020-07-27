using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    [BsonIgnoreExtraElements]
    [BsonCollection("StrainTypes")]
    public class StrainType: BaseDocument
    {
    }
}
