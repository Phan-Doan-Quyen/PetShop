using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Utilities;

namespace PetShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly PetShopContext _context;
        public LoginController(PetShopContext context)
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
            //Mã hóa mật khẩu trước khi kiểm tra
            string pw = Function.MD5Password(account.Password);
            //Kiểm tra sự tồn tại của email trong csdl
            var check = _context.TbAccounts.Where(m => (m.Email == account.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                //HIện thị thông báo
                Function._Message = "Tên tài khoản hoặc mật khẩu không hợp lệ!";
                return RedirectToAction("Index", "Login");
            }
            //Vào trang admin nếu đúng email và password
            Function._Message = string.Empty;
            Function._AccountId = check.AccountId;
            Function._FullName = string.IsNullOrEmpty(check.FullName) ? string.Empty : check.FullName;
            Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            if (check != null)
            {
                if (check.RoleId == 1)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
