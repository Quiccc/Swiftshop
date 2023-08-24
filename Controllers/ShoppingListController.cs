using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public ShoppingListController(SwiftshopDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(int Page)
        {
            if (Page == 0) { return RedirectToAction("Index", new { Page = 1 }); }

            int PaginatorSize;
            var Email = _contextAccessor.HttpContext?.User.Identity?.Name;
            //ID of the authenticated user.
            var CurrentUserId = _userManager.FindByEmailAsync(Email).Result?.Id;

            //Fetch all user list in descending order.
            var UserLists = await _context.ShoppingLists
                .Where(sl => sl.UserId == CurrentUserId)
                .OrderBy(sl => sl.CreatedAt)
                .ToListAsync();

            //Calculating the paginator length for paginator component.
            PaginatorSize = UserLists.Count / 3;
            if (UserLists.Count % 3 != 0) { PaginatorSize++; }
            ViewBag.PaginatorSize = PaginatorSize;

            //Fetching up to 4 Shopping Lists per page.
            List<ShoppingList>? PageLists = new();
            for (int i = (Page - 1) * 3; i < Page * 3; i++)
            {
                if (UserLists.Count <= i) { break; } // Null Exception Preventation.

                PageLists.Add(UserLists.ElementAt(i));
            }

            return View(PageLists);
        }

        //Functionality for adding lists to favorites.
        [Authorize]
        public async Task<IActionResult> SwitchIsFavorite(string ListId, bool OldStatus)
        {
            var ShoppingListContext = _context.ShoppingLists.First(sl => sl.Id == ListId).IsFavorited = !OldStatus;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ShoppingList");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}