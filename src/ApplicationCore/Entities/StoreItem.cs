using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using NetTopologySuite.Geometries;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class StoreItem: BaseEntity, IAggregateRoot
    {        
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public string Description { get; set; }
        public string License { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string DeliveryZipCode { get; set; }
        public string StoreSKU { get; set; }
        //public Point Location { get; set; }        

        public StoreItem(string name, string pictureUri, string description, string license, string state,
            string city, string address, int zipCode, string deliveryZipCode, string storeSKU) //, Point location)
        {
            Name = name;
            PictureUri = pictureUri;
            Description = description;
            License = license;
            State = state;
            City = city;
            Address = address;
            ZipCode = zipCode;
            DeliveryZipCode = deliveryZipCode;
            StoreSKU = storeSKU;
            //Location = location;            
        }

        public void Update(string name, string city)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Name = name;
            City = city;
        }
    }
}
