using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Itemimage
    {
        public int ItemImageId { get; set; }
        public string? ItemImageUrl { get; set; }
        public int? ItemId { get; set; }

        public virtual Item? Item { get; set; }
    }
}
