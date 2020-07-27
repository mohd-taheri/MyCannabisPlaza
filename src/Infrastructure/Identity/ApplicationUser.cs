using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using System;

namespace Microsoft.eShopWeb.Infrastructure.Identity
{
    public class ApplicationUser : MongoUser
    {        
        public byte[] IDCard { get; set; }
        public byte[] MedicalLetter { get; set; }

        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string userName, string email) : base(userName)
        {
        }
    }
}
