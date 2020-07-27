using System;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate
{
    public class Address // ValueObject
    {
        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        
        public int ZipCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public byte[] IDCard { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool Agreement { get; private set; }

        private Address() { }

        public Address(string state,string city,string street, int zipcode, string phoneNumber, 
            byte[] idCard,string email, DateTime birthDate,bool agreement)
        {
            Street = street;
            City = city;
            State = state;
            Country = "USA";
            ZipCode = zipcode;
            PhoneNumber = phoneNumber;
            IDCard = idCard;
            Email = email;
            BirthDate = birthDate;
            Agreement = agreement;
        }
    }
}
