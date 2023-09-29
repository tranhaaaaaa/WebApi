using FinalApi.Dto;
using FinalApi.Models;

namespace FinalApi
{
    public class OrderResponse
    {
        public List<GetOrderRequest> Orders { get; set; }
        public int Page { get; set; }   
        public int CurrentPage { get; set; }
    }
}
