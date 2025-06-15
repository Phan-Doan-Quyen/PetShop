using PetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly PetShopContext _context;

        public ProductController(PetShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.TbProducts.ToList(); 
            return View(products.OrderByDescending(m => m.ProductId));
        }

        [Route("/product/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }

            var product = await _context.TbProducts.Include(i => i.CategoryProduct)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.productReview = _context.TbProductReviews.
                Where(i => i.ProductId == id && i.IsActive).ToList();
            ViewBag.productRelated = _context.TbProducts.
                Where(i => i.ProductId != id && i.CategoryProductId == product.CategoryProductId).Take(5).
                OrderByDescending(i => i.ProductId).ToList();
            return View(product);
        }
    }
}
