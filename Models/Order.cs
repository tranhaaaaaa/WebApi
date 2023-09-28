using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Order
    {
        internal readonly object ItemDetails;

        public Order()
        {
            Orderdetails = new HashSet<Orderdetail>();
            Payments = new HashSet<Payment>();
            Shippings = new HashSet<Shipping>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public int? AddressId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OverDueDate { get; set; }

        public virtual Address? Address { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
