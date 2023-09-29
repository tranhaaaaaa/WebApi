using FinalApi.Models;
using FinalApi.Request;
using FinalApi.Services;

namespace FinalApi.Repository
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly ICustomerService _customerServices;
            

        public CustomerRepository(ICustomerService customerServices)
        {
          _customerServices = customerServices;
        }

        public async Task<IEnumerable<CustomerRequest>> CreateCustomerVipAsync()
        {
            return await _customerServices.CreateCustomerVipAsync();
        }
    }
}
