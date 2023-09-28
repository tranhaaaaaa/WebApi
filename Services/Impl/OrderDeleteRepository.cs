using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Services.Repository;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FinalApi.Services.Impl
{
    public class OrderDeleteRepository : GenerRepository<Order>, IOrderDeleteRepository
    {
        private readonly projectDemoContext _context;
        public OrderDeleteRepository(projectDemoContext context) : base(context)
        {
            _context = context;
        }

        public void RemoveItemFromOrder(int idOrder, int itemId)
        {
            var DeleteItem = (from o in _context.Orders
                              join od in _context.Orderdetails on o.OrderId equals od.OrderId
                              join it in _context.Itemdetails on od.ItemDetailId equals it.ItemDetailId
                              join il in _context.Items on it.ItemId equals il.ItemId
                              join s in _context.Shops on it.ShopId equals s.ShopId
                              join c in _context.Customers on o.CustomerId equals c.CustomerId

                              select new
                              {
                                  o.OrderId,
                                  od.ItemDetailId,
                                  c.CustomerName,
                                  o.TotalPrice,
                                  o.OverDueDate,
                                  o.Status,
                                  il.ItemId,
                                  il.ItemName,
                                  it.Color,
                                  it.Quantity,
                                  s.ShopName,
                                  it.Size,
                              }).ToList();
            var x = DeleteItem.FirstOrDefault(o => o.OrderId == idOrder);
            _context.Orderdetails.FirstOrDefault(o => o.OrderId == idOrder);
            _context.Orderdetails.Remove(_context.Orderdetails.FirstOrDefault(o => o.OrderId == idOrder));
            _context.SaveChanges();
            
        }
    }
}
