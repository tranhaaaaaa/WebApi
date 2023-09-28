using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Item
    {
        public Item()
        {
            Cartitems = new HashSet<Cartitem>();
            Itemdetails = new HashSet<Itemdetail>();
            Itemimages = new HashSet<Itemimage>();
        }

        public int? ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime? ImportDate { get; set; }
        public double? Discount { get; set; }
        public byte? IsAvailable { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Cartitem> Cartitems { get; set; }
        public virtual ICollection<Itemdetail> Itemdetails { get; set; }
        public virtual ICollection<Itemimage> Itemimages { get; set; }
    }
}
