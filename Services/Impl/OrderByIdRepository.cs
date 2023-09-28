using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Services.Repository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace FinalApi.Services.Impl
{
    public class OrderByIdRepository : GenerRepository<GetOrderRequest>, IOrderByIdRepository
    {
        private readonly projectDemoContext _context;
        public OrderByIdRepository(projectDemoContext context) : base(context)
        {
            _context = context;
        }
        IEnumerable<GetOrderRequest> IGenerRepository<GetOrderRequest>.GetOrders()
        {
            var orders = (from o in _context.Orders
                          join od in _context.Orderdetails on o.OrderId equals od.OrderId
                          join it in _context.Itemdetails on od.ItemDetailId equals it.ItemDetailId
                          join s in _context.Shops on it.ShopId equals s.ShopId
                          group new { o, od, it, s } by new
                          {
                              od.OrderId,
                              s.ShopName,
                              o.OverDueDate,
                              o.Status
                          } into groupedItems
                          select new GetOrderRequest
                          {
                              OrderId = groupedItems.Key.OrderId,
                              ShopName = groupedItems.Key.ShopName,
                              OverDueDate = groupedItems.Key.OverDueDate,
                              Status = groupedItems.Key.Status,
                              Quantity = groupedItems.Sum(ia => ia.od.Quantity),
                              TotalPrice = groupedItems.Sum(ia => ia.od.Quantity * ia.it.ItemPrice)

                          }).ToList();

            return orders;
        }
        GetOrderRequest IGenerRepository<GetOrderRequest>.GetOrderById(int id)
        {
            var GetIdItem= (from o in _context.Orders
                                join od in _context.Orderdetails on o.OrderId equals od.OrderId
                                join it in _context.Itemdetails on od.ItemDetailId equals it.ItemDetailId
                                join il in _context.Items on it.ItemId equals il.ItemId
                                join s in _context.Shops on it.ShopId equals s.ShopId
                                join c in _context.Customers on o.CustomerId equals c.CustomerId
                                where o.OrderId == id
                                select new
                                {
                                    o.OrderId,
                                    c.CustomerName,
                                    o.OverDueDate,
                                    o.Status,
                                    il.ItemId,
                                    il.ItemName,
                                    it.Color,
                                    o.TotalPrice,
                                    it.ItemPrice,
                                    od.Quantity,
                                    s.ShopName,
                                    it.Size,
                                }).ToList();
            var orderDto = new GetOrderRequest
            {
                OrderId = GetIdItem.First().OrderId,
                OverDueDate = GetIdItem.First().OverDueDate,
                Status = GetIdItem.First().Status,
                ShopName = GetIdItem.First().ShopName,
                Item = GetIdItem.Select(od => new ItemDto
                {
                    itemId = od.ItemId,
                    ItemName = od.ItemName,
                    Size = od.Size,
                    Color = od.Color,
                    Quantity = od.Quantity,
                    Price = od.ItemPrice
                }).ToList(),
                TotalPrice = GetIdItem.Sum(od => od.Quantity * od.ItemPrice),
                Quantity = GetIdItem.Sum(od => od.Quantity)
            };

            return orderDto;
        }
    }
}
