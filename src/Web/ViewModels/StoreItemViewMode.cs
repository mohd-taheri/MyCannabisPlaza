using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class StoreItemViewModel
    {
        public ObjectId Id { get; set; }
        public int SecondaryId { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public string Description { get; set; }
        public string License { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public DeliveryZipCode[] DeliveryZipCode { get; set; }
        
    }
    
}
