using FinalApi.Request;

namespace FinalApi.Services.Repository
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerRequest>> CreateCustomerVipAsync();
    }
}
