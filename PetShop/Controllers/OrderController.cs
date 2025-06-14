using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Utilities;

namespace DACN.Controllers
{
    public class OrderController : Controller
    {
        private readonly PetShopContext _context;

        public OrderController(PetShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.TbProducts.ToList();
            ViewBag.product = products;

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.cartItems = cartItems;

            decimal cartTotal = 0;
            int itemCount = 0;
            foreach (var item in cartItems)
            {
                decimal itemPrice = item.PriceSale.HasValue && item.PriceSale.Value > 0 ? (decimal)item.PriceSale.Value : (decimal)item.Price;
                cartTotal += itemPrice * item.Quantity;
                itemCount += item.Quantity;
            }
            ViewBag.CartTotal = cartTotal;
            ViewBag.ItemCount = itemCount;

            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = _context.TbProducts.Find(id);
            if (product != null)
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                var cartItem = cart.FirstOrDefault(x => x.ProductId == id);
                if (cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        ProductId = product.ProductId,
                        Alias = product.Alias,
                        Title = product.Title,
                        Image = product.Image,
                        Price = product.Price.GetValueOrDefault(),
                        PriceSale = product.PriceSale,
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var itemToRemove = cart.FirstOrDefault(x => x.ProductId == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int change)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                item.Quantity = Math.Max(1, item.Quantity + change);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout(string phone, string address)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (cartItems.Count == 0)
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index", "Order");
            }

            // Kiểm tra số lượng tồn kho
            foreach (var item in cartItems)
            {
                var product = _context.TbProducts.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (product == null)
                {
                    TempData["Error"] = $"Sản phẩm với ID {item.ProductId} không tồn tại.";
                    return RedirectToAction("Index", "Order");
                }

                if (product.Quantity < item.Quantity)
                {
                    TempData["Error"] = $"Sản phẩm '{product.Title}' chỉ còn {product.Quantity} sản phẩm trong kho. Vui lòng cập nhật giỏ hàng.";
                    return RedirectToAction("Index", "Order");
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tính tổng tiền (decimal), sau đó convert sang int?
                    var totalAmountDecimal = cartItems.Sum(x => ((x.PriceSale ?? x.Price) * x.Quantity));
                    int totalAmountInt = Convert.ToInt32(totalAmountDecimal);

                    var order = new TbOrder
                    {
                        Phone = phone,
                        Address = address,
                        CreatedDate = DateTime.Now,
                        TotalAmount = totalAmountInt,
                        Quanlity = cartItems.Sum(x => x.Quantity),
                        OrderStatusId = 1 // trạng thái chờ xử lý
                    };

                    _context.TbOrders.Add(order);
                    _context.SaveChanges();

                    foreach (var item in cartItems)
                    {
                        var product = _context.TbProducts.First(p => p.ProductId == item.ProductId);

                        // Cập nhật tồn kho
                        product.Quantity -= item.Quantity;
                        product.UnitInStock += item.Quantity;

                        var detail = new TbOrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = item.ProductId,
                            Price = (decimal)(item.PriceSale ?? item.Price),
                            Quantity = item.Quantity
                        };

                        _context.TbOrderDetails.Add(detail);
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    HttpContext.Session.Remove("Cart");

                    return RedirectToAction("OrderSuccess");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    TempData["Error"] = "Có lỗi xảy ra khi xử lý đơn hàng. Vui lòng thử lại sau.";
                    return RedirectToAction("Index", "Order");
                }
            }
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
