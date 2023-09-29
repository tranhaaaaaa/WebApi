using FinalApi.Dto;

namespace FinalApi.Services
{
    public interface IOrderServices
    {
        GetOrderRequest GetOrderById(int id);
        CreateOrderRequest CreateOrders(CreateOrderRequest request);
        void RemoveItemFromOrder(int idOrder, int itemId);
        IEnumerable<OrderDto> GetOrderByItem(string Name);

        IEnumerable<GetOrderRequest> GetOrders();
       
        
        
    }
}
