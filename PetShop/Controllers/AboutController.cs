using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
