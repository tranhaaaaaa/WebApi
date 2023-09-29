using FinalApi.Models;
using FinalApi.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalApi.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerRequest>> CreateCustomerVipAsync();
    }
}
