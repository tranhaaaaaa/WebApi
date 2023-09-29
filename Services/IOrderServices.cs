using FinalApi.Dto;
using FinalApi.Models;
using FinalApi.Services.Repository;

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
