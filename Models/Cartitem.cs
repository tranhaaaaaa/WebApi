using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Cartitem
    {
        public int CartItemId { get; set; }
        public int? CartId { get; set; }
        public int? ItemId { get; set; }
        public int? Quantity { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual Item? Item { get; set; }
    }
}
