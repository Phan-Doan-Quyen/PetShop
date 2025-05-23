using PetShop.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace PetShop.ViewComponents
{
    public class ReViewViewComponent : ViewComponent
    {
        private readonly PetShopContext _context;
        public ReViewViewComponent(PetShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbContacts;
            return await Task.FromResult<IViewComponentResult>
               (View(items.OrderByDescending(m => m.ContactId).ToList()));

        }
    }
}