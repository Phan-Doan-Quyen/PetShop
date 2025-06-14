using System;

namespace PetShop.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }  // nếu dùng trong DB

        public int ProductId { get; set; }

        public string? Alias { get; set; }

        public string Title { get; set; }
        public string Image { get; set; }

        // Kiểu decimal cho tiền tệ
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }

        public int Quantity { get; set; }

        // Tổng tiền cho item này (có thể tính trong property)
        public decimal Total => Quantity * (PriceSale ?? Price);
    }
}
