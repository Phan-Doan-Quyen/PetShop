using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Controllers
{
    public class CartController : Controller
    {
        private readonly PetShopContext _context;

        public CartController(PetShopContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var carts = _context.TbOrderDetails.Include(t => t.Product).ToList();
            return View(carts);
        }
    }
}
