namespace PetShop.Models
{
    public class CheckoutViewModel
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
