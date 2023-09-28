using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Order>();
        }

        public int AddressId { get; set; }
        public int? CustomerId { get; set; }
        public string? Province { get; set; }
        public string? Town { get; set; }
        public string? District { get; set; }
        public string? SpecificAdd { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
