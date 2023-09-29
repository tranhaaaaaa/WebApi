using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Shipping
    {
        public int ShippingId { get; set; }
        public string ShippingName { get; set; }
        public string ShippingStatus { get; set; }
        public double? ShippingFee { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
