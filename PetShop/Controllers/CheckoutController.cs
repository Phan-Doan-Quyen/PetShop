using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly PetShopContext _context;

        public CheckoutController(PetShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var checkouts = _context.TbOrderDetails.Include(t => t.Product).ToList();
            return View(checkouts);
        }
    }
}
