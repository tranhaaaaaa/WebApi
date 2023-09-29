using FinalApi.Dto;

namespace FinalApi.Services.Repository
{
    public interface IOrderServices
    {
        GetOrderRequest GetOrderById(int id);
        CreateOrderRequest CreateOrders(CreateOrderRequest request);
        void RemoveItemFromOrder(int idOrder, int itemId);
        IEnumerable<OrderDto> GetOrderByItem(string Name);

        IEnumerable<GetOrderRequest> GetOrders(int pageNumber, int page);



    }
}
