using Store.Data.Entities.IdentityEntities;
using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities.OrderEntities
{
    public class ShippingAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}