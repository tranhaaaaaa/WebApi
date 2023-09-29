using FinalApi.Request;

namespace FinalApi.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerRequest>> CreateCustomerVipAsync();
    }
}
