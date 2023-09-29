using FinalApi.Models;
using FinalApi.Repository;
using FinalApi.Request;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly projectDemoContext _context;
        public CustomerService(projectDemoContext context)
        {
         _context = context;
        }

        public Task<IEnumerable<CustomerRequest>> CreateCustomerVipAsync()
        {
            var vipCustomers = _context.Customers
               .Where(c => _context.Orders
                   .Where(o => _context.Orderdetails.Any(od => od.OrderId == o.OrderId))
                   .GroupBy(o => o.CustomerId)
                   .Where(g => g.Sum(o => o.TotalPrice) > 4 || g.SelectMany(o => o.Orderdetails).Select(od => od.ItemDetailId).Distinct().Count() > 2)
                   .Select(g => g.Key)
                   .Contains(c.CustomerId))
               .ToList();

            foreach (var customer in vipCustomers)
            {
                customer.CustomerType = 1;
            }
             _context.SaveChangesAsync();

            return null;
          
        }
    }
}
