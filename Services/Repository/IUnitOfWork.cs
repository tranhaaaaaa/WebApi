namespace FinalApi.Services.Repository
{
    public interface IUnitOfWork :IDisposable
    {
         IOrderCreateRepository CreateOrderRequest { get; }
        IOrderGetByItemRepository OrderGetByItem { get; }
        ICustomerVipRepository CustomerRequest { get; }
       IOrderByIdRepository OrderById { get; }
        IOrderDeleteRepository OrderDeleteRequest { get; }
        int save();
        
    }
}
