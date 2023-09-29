namespace FinalApi.Dto
{
    public class CreateOrderRequest
    {    
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public int? AddressId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OverDueDate { get; set; }
        public List<CreateOrderItemRequest> OrderItems { get; set; }
    }
}
