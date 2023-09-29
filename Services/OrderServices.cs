using FinalApi.Dto;
using FinalApi.Models;

namespace FinalApi.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly projectDemoContext _context;
     
        public OrderServices(projectDemoContext context) 
        { 
            _context = context; 
         
        }  
        public CreateOrderRequest CreateOrders(CreateOrderRequest request)
        {
            var createdOrder = new Order
            {
                OrderDate = request.OrderDate,
                CustomerId = request.CustomerId,
                AddressId = request.AddressId,
                OverDueDate = request.OverDueDate,
                Status = request.Status,
                Orderdetails = request.OrderItems.Select(item => new Orderdetail
                {
                    ItemDetailId = item.ItemId,
                    Quantity = item.Quantity,
                }).ToList()
            };
            _context.Orders.Add(createdOrder);
            _context.SaveChanges();
            return null;
          
        }
        public GetOrderRequest GetOrderById(int id)
        {
            var GetIdItem = (from o in _context.Orders
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
        public IEnumerable<OrderDto> GetOrderByItem(string Name)
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
        public IEnumerable<GetOrderRequest> GetOrders()
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
