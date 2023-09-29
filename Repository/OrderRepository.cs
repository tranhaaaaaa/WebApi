using FinalApi.Dto;
using FinalApi.Services;


namespace FinalApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderServices _orderServices;
        public OrderRepository(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        public CreateOrderRequest CreateOrders(CreateOrderRequest request)
        {
            return _orderServices.CreateOrders(request);
        }
        public GetOrderRequest GetOrderById(int id)
        {
            return _orderServices.GetOrderById(id);
        }
        public IEnumerable<GetOrderRequest> GetOrders()
        {
            return _orderServices.GetOrders();
        }
        public void RemoveItemFromOrder(int idOrder, int itemId)
        {
            _orderServices.RemoveItemFromOrder(idOrder, itemId);
        }
         public IEnumerable<OrderDto> GetOrderByItems(string Name)
        {
            return _orderServices.GetOrderByItem(Name);
        }     
        
    }
}
