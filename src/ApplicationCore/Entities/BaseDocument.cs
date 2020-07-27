using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public abstract class BaseDocument : IBaseDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
        public DateTime UpdatedAt => Id.CreationTime;
    }
}
