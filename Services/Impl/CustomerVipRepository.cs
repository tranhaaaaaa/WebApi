using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Request;
using FinalApi.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinalApi.Services.Impl
{
    public class CustomerVipRepository : GenerRepository<CustomerRequest>, ICustomerVipRepository
    {
        private projectDemoContext _context;
        public CustomerVipRepository(projectDemoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerRequest>> CreateCustomerVipAsync()
        {
            var vipCustomers = await _context.Customers
                    .Where(c => _context.Orders
                        .Where(o => _context.Orderdetails.Any(od => od.OrderId == o.OrderId))
                        .GroupBy(o => o.CustomerId)
                        .Where(g => g.Sum(o => o.TotalPrice) > 4 || g.SelectMany(o => o.Orderdetails).Select(od => od.ItemDetailId).Distinct().Count() > 2)
                        .Select(g => g.Key)
                        .Contains(c.CustomerId))
                    .ToListAsync();

                foreach (var customer in vipCustomers)
                {
                    customer.CustomerType = 1;
                }

                await _context.SaveChangesAsync();
                return null;
        }
          
    }

}



