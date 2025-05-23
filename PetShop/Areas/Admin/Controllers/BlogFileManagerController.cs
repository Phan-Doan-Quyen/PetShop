using Microsoft.AspNetCore.Mvc;

namespace PetShop.Areas.Admin.Controllers
{
    public class BlogFileManagerController : Controller
    {
        [Area("Admin")]
        [Route("/Admin/blog-file-manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
