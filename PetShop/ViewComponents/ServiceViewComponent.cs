using PetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetShop.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly PetShopContext _context;

        public ServiceViewComponent(PetShopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbServices.Include(m => m.CategoryService)
                .Where(m => (bool)m.IsActive);
            return await Task.FromResult<IViewComponentResult>
                (View(items.OrderBy(m => m.ServiceId).ToList()));
        }
    }
}
