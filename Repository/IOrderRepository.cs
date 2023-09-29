using FinalApi.Dto;
using FinalApi.Models;

namespace FinalApi.Repository
{
    public interface IOrderRepository
    {
        GetOrderRequest GetOrderById(int id);
        CreateOrderRequest CreateOrders(CreateOrderRequest CreateOrderRequest);
        void RemoveItemFromOrder(int idOrder, int itemId);
        IEnumerable<OrderDto> GetOrderByItems(string Name);

        IEnumerable<GetOrderRequest> GetOrders();

    }
}
