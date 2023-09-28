using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Services.Repository;

using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace FinalApi.Services.Impl
{
    public class OrderGetByItemRepository : GenerRepository<OrderDto>, Repository.IOrderGetByItemRepository
    {
        private readonly projectDemoContext _context;
        public OrderGetByItemRepository(projectDemoContext context) : base(context)
        {
            _context = context;
        }
        IEnumerable<OrderDto> IGenerRepository<OrderDto>.GetOrderByItem(string Name)
        {
            var searchResults = (from o in _context.Orders
                                 join od in _context.Orderdetails on o.OrderId equals od.OrderId
                                 join it in _context.Itemdetails on od.ItemDetailId equals it.ItemDetailId
                                 join il in _context.Items on it.ItemId equals il.ItemId
                                 join s in _context.Shops on it.ShopId equals s.ShopId
                                 where il.ItemName.Contains(Name)
                                 group new { o, il, it, s } by new { o.OrderId, o.OverDueDate, o.Status, o.Customer.CustomerName, o.Customer.PhoneNumber } into grouped
                                 select new OrderDto
                                 {
                                     OrderId = grouped.Key.OrderId,
                                     TotalPrice = grouped.Sum(item => item.it.ItemPrice * item.it.Quantity),
                                     OverDueDate = grouped.Key.OverDueDate,
                                     Status = grouped.Key.Status,
                                     CustomerName = grouped.Key.CustomerName,
                                     PhoneNumber = grouped.Key.PhoneNumber,
                                     ShopName = grouped.First().s.ShopName,
                                     Item = grouped.Select(item => new ItemDto
                                     {
                                         itemId = item.il.ItemId,
                                         ItemName = item.il.ItemName,
                                         Color = item.it.Color,
                                         Quantity = item.it.Quantity,
                                         Size = item.it.Size,
                                         Price = item.it.ItemPrice
                                     }).ToList()
                                 }).ToList();

            if (searchResults == null || !searchResults.Any())
            {
                return null;
            }

            return searchResults;
        }


    }
}

