using FinalApi.Dto;
using FinalApi.Models;

namespace FinalApi.Response
{
    public class OrderResponse
    {
        public List<GetOrderRequest> Orders { get; set; }
    }
}
