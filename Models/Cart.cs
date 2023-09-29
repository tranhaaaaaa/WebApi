using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Cart
    {
        public Cart()
        {
            Cartitems = new HashSet<Cartitem>();
        }

        public int CartId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Cartitem> Cartitems { get; set; }
    }
}
