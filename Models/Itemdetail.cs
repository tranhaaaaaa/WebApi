using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Itemdetail
    {
        public Itemdetail()
        {
            Orderdetails = new HashSet<Orderdetail>();
        }

        public int ItemDetailId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int? Quantity { get; set; }
        public int? ItemId { get; set; }
        public double? ItemPrice { get; set; }
        public int? ShopId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
    }
}
