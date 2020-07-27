using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    [BsonIgnoreExtraElements]
    [BsonCollection("stores")]
    public class Store: BaseDocument
    {       
        public int SecondaryId { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public string Description { get; set; }
        public string License { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public DeliveryZipCode[] DeliveryZipCode { get; set; }
        public WorkingHours[] WorkingHours { get; set; }
        public decimal MinimumOrder { get; set; }
        public bool HasDelivery { get; set; }
        public bool HasPickup { get; set; }
        public bool IsActive { get; set; }       
    }

    public class WorkingHours
    {
        public int DayOfWeek { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }

    }

    public class DeliveryZipCode
    {
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public int[] ZipCode { get; set; }        
        public decimal MinOrder { get; set; }
    }
}
