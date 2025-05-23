using System;

namespace PetShop.Models
{
    [Serializable]
    public class CartItem
    {
        public long ProductId { set; get; }
        public int Quantity { set; get; }
    }
}
