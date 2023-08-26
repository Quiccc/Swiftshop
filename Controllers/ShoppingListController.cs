using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swiftshop.Database;
using Swiftshop.Models;
using System.Diagnostics;

namespace Swiftshop.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly SwiftshopDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public ShoppingListController(SwiftshopDbContext context, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> CreateShoppingList(string NewName)
        {
            var ShoppingListContext = _context.ShoppingLists;

            var CurrentUser = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            var ActiveListsOfUser = _context.ShoppingLists
                .Where(al => al.UserId == CurrentUser.Id && al.IsActive == true)
                .Select(al => al.Name)
                .ToList();

            if (ActiveListsOfUser.Contains(NewName))
            {
                return RedirectToAction("Index", "LandingPage", new { Page = 1 });
            }

            ShoppingList NewList = new()
            {
                Name = NewName,
                UserId = CurrentUser.Id,
                User = CurrentUser
            };

            await _context.AddAsync(NewList);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "LandingPage", new { Page = 1 });
        }
    }
}
