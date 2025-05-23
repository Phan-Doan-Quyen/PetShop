using PetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace PetShop.Controllers
{
    public class BlogController : Controller
    {
        private readonly PetShopContext _context;
        public BlogController(PetShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = _context.TbBlogs.ToList(); 
            return View(blogs);
        }

        [Route("/blog/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBlogs == null)
            {
                return NotFound();
            }

            var blog = await _context.TbBlogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewBag.blogComment = _context.TbBlogComments.Where(i => i.BlogId == id && i.IsActive).ToList();
            ViewBag.blogCategory = _context.TbCategories.ToList();
            ViewBag.blog = _context.TbBlogs.ToList();
            return View(blog);
        }
    }
}
