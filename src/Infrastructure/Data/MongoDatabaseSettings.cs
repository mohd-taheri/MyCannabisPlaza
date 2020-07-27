using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string ProductCollection { get; set; }
        public string StoreCollection { get; set; }
        public string BasketCollection { get; set; }
        public string OrderCollection { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string ProductCollection { get; set; }
        string StoreCollection { get; set; }
        string BasketCollection { get; set; }
        string OrderCollection { get; set; }
    }
}
