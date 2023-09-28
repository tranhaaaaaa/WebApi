namespace FinalApi.Services.Repository
{
    public interface IUnitOfWork :IDisposable
    {
         IOrderCreateRepository CreateOrderRequest { get; }
        IOrderGetByItemRepository OrderDto { get; }
        ICustomerVipRepository CustomerRequest { get; }
       IOrderByIdRepository GetOrderRequest { get; }
        IOrderDeleteRepository OrderDeleteRequest { get; }
        int save();
        
    }
}
