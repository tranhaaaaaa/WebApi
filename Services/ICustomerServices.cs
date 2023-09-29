using FinalApi.Request;
namespace FinalApi.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerRequest>> CreateCustomerVipAsync();
    }
}
