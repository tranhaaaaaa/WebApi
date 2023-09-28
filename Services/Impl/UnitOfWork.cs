using FinalApi.Models;
using FinalApi.Services.Repository;

namespace FinalApi.Services.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly projectDemoContext _context;
        public UnitOfWork(projectDemoContext context)
        { 
            _context = context;
            OrderById = new OrderByIdRepository(_context);
            CreateOrderRequest = new OrderCreateRepository(_context);
            CustomerRequest = new CustomerVipRepository(_context);
            OrderGetByItem = new OrderGetByItemRepository(_context);
            OrderDeleteRequest = new OrderDeleteRepository(_context);
        }
        public IOrderByIdRepository OrderById { get; set; } 
        public IOrderCreateRepository CreateOrderRequest { get; set; }
        public Repository.IOrderGetByItemRepository OrderGetByItem { get; set; }
        public ICustomerVipRepository CustomerRequest { get; set; }
        

        public IOrderDeleteRepository OrderDeleteRequest { get; set; }

        public int save()
        {
            return _context.SaveChanges();  
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
