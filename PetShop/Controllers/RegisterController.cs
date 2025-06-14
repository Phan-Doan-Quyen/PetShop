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
        public IActionResult Index(TbAccount account)
        {
            if (account == null)
            {
                return NotFound();
            }
            //Kiểm tra sự tồn tại của email trong csdl
            var check = _context.TbAccounts.Where(m => m.Email == account.Email).FirstOrDefault();
            if (check != null)
            {
                //HIện thị thông báo
                Function._MessageEmail = "Đã có tài khoản sử dụng Email này!";
                return RedirectToAction("Index", "Register");
            }
            //Nếu ko có thì thêm vào csdl
            Function._MessageEmail = string.Empty;
            account.Password = Function.MD5Password(account.Password);
            _context.Add(account);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
}
