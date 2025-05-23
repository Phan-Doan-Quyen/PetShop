using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class ServiceController : Controller
    {
        private readonly PetShopContext _context;
        public ServiceController (PetShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/service/{Title}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbServices == null)
            {
                return NotFound();
            }

            var service = await _context.TbServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }
            ViewBag.ServiceReview = _context.TbServiceReviews.Where(i => i.ServiceId == id && i.IsActive).ToList();
            ViewBag.ServiceCategory = _context.TbServiceCategories.ToList();  
            ViewBag.service = _context.TbServices.ToList();
            return View(service);
        }
    }
}



       

      
