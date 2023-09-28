namespace FinalApi.Dto
{
    public class OrderDto
    {
       
           public int OrderId { get; set; } 
            public double? TotalPrice { get; set; }
           public int? Quantity { get; set; }
             public string CustomerName { get; set; }
            public DateTime OverDueDate { get; set; }
            public string Status { get; set; }
            public string ShopName { get; set; }
            public int? PhoneNumber { get; set; }
     
        public List<ItemDto> Item { get; set; }
     

       
    }
    
}
