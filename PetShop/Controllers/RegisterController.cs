using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Utilities;

namespace PetShop.Controllers
{
    public class RegisterController : Controller
    {
        private readonly PetShopContext _context;
        public RegisterController(PetShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TbCustomer customer)
        {
            if (customer == null)
            {
                return NotFound();
            }
            //Kiểm tra sự tồn tại của email trong csdl
            var check = _context.TbCustomers.Where(m => m.Email == customer.Email).FirstOrDefault();
            if (check != null)
            {
                //HIện thị thông báo
                Function._MessageEmail = "Đã có tài khoản sử dụng Email này!";
                return RedirectToAction("Index", "Register");
            }
            //Nếu ko có thì thêm vào csdl
            Function._MessageEmail = string.Empty;
            customer.Password = Function.MD5Password(customer.Password);
            _context.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
}
