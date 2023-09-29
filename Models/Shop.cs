
namespace FinalApi.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Itemdetails = new HashSet<Itemdetail>();
        }

        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? PhoneNumber { get; set; }
        public string Address { get; set; }
        public double? Revenue { get; set; }

        public virtual ICollection<Itemdetail> Itemdetails { get; set; }
    }
}
