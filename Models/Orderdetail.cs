using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Orderdetail
    {
        public int OrderDetailId { get; set; }
        public int? Quantity { get; set; }
        public double Discount { get; set; }
        public int OrderId { get; set; }
        public int ItemDetailId { get; set; }

        public virtual Itemdetail ItemDetail { get; set; }
        public virtual Order Order { get; set; }
    }
}
