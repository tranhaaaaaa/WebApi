using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Request;
using FinalApi.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web.Http.Results;

namespace FinalApi.Services.Impl
{
    public class GenerRepository<T> : IGenerRepository<T> where T : class
    { 
        private readonly projectDemoContext _context;
        public GenerRepository(projectDemoContext context)
        {
            _context = context;
        }
        public T CreateOrder(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges(); 
            return entity;
        }
        public void  RemoveItemFromOrder(int orderId, int itemId)
        {
              _context.Set<T>().RemoveRange();
        }
        public T GetOrderById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetOrderByItem(string Name)
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetOrders()
        {
            return _context.Set<T>().ToList();
        }

     
        async Task<IEnumerable<T>> IGenerRepository<T>.CreateCustomerVipAsync()
        {
            var vipCustomers = await _context.Set<T>().ToListAsync();

            return vipCustomers;
        }
    }
}
