using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Services.Repository;

namespace FinalApi.Services.Impl
{
    public class OrderCreateRepository : GenerRepository<CreateOrderRequest>, IOrderCreateRepository
    {
        private readonly projectDemoContext _context;
        public OrderCreateRepository(projectDemoContext context) : base(context)
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

    }
}
