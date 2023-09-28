using FinalApi.Dto;

namespace FinalApi.Request
{
    public class CustomerRequest
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int? PhoneNumber { get; set; }
        public int? CustomerType { get; set; }
        public string Passwords { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
