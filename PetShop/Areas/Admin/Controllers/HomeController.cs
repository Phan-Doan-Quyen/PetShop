using Microsoft.AspNetCore.Mvc;
using PetShop.Utilities;

namespace PetShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            if (!Function.IsLogin())
                return RedirectToAction("Index", "Login");
            return View();
        }

        public IActionResult Logout()
        {
            Function._AccountId = 0;
            Function._Email = string.Empty;
            Function._FullName = string.Empty;
            Function._Phone = string.Empty;
            Function._Message = string.Empty;
            Function._MessageEmail = string.Empty;

            return RedirectToAction("Index", "Home");
        }
    }
}
