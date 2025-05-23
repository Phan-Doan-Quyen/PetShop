using PetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetShop.ViewComponents
{
    public class TeamViewComponent : ViewComponent
    {
        private readonly PetShopContext _context;

        public TeamViewComponent(PetShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbAccounts.Include(m => m.Role)
                .Where(m => (bool)m.IsActive);
            return await Task.FromResult<IViewComponentResult>
                (View(items.OrderBy(m => m.AccountId).ToList()));
        }
    }
}

