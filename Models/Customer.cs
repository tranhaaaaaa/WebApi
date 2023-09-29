using System;
using System.Collections.Generic;

namespace FinalApi.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Passwords { get; set; }
        public int? PhoneNumber { get; set; }
        public byte? LockOutEnable { get; set; }
        public DateTime? LockOutEnd { get; set; }
        public int? AccessFailedCount { get; set; }
        public int? CustomerType { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
