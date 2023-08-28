using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swiftshop.Database;
using Swiftshop.Models;
using System.Diagnostics;

namespace Swiftshop.Controllers
{
    [Authorize(Roles = "Admin, User")]
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

            ShoppingList NewList = new()
            {
                Name = NewName,
                UserId = CurrentUser.Id,
                User = CurrentUser
            };

            await _context.AddAsync(NewList);
            await _context.SaveChangesAsync();

            return RedirectToAction("ActiveLists", new { Page = 1 });
        }

        public async Task<IActionResult> ActiveLists(int Page)
        {
            if (Page == 0) 
            {
                ViewBag.Page = 1;
                return RedirectToAction("ActiveLists", new { Page = 1 }); 
            }

            int PaginatorSize;

            //Get Current User
            var CurrentUser = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            //Fetch all user list newest to oldest.
            var UserLists = await _context.ShoppingLists
                .Where(sl => sl.UserId == CurrentUser.Id && sl.IsActive == true && sl.IsStarted == false)
                .OrderBy(sl => sl.CreatedAt)
                .ToListAsync();

            //Calculating the paginator length for paginator component.
            PaginatorSize = UserLists.Count / 3;
            if (UserLists.Count % 3 != 0) { PaginatorSize++; }
            ViewBag.PaginatorSize = PaginatorSize;

            //Fetching up to 3 Shopping Lists per page.
            List<ShoppingList>? PageLists = new();
            for (int i = (Page - 1) * 3; i < Page * 3; i++)
            {
                if (UserLists.Count <= i) { break; } // Null Exception Preventation.

                PageLists.Add(UserLists.ElementAt(i));
            }

            ViewBag.Page = Page;
            return View(PageLists);
        }

        public async Task<IActionResult> CompletedLists(int Page)
        {
            if (Page == 0) { return RedirectToAction("CompleteLists", new { Page = 1 }); }

            int PaginatorSize;

            var CurrentUser = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            //Fetch all user list in newest to oldest.
            var UserLists = await _context.ShoppingLists
                .Where(sl => sl.UserId == CurrentUser.Id && sl.IsStarted == true && sl.IsActive == true)
                .OrderBy(sl => sl.CreatedAt)
                .ToListAsync();

            //Calculating the paginator length for paginator component.
            PaginatorSize = UserLists.Count / 3;
            if (UserLists.Count % 3 != 0) { PaginatorSize++; }
            ViewBag.PaginatorSize = PaginatorSize;

            //Fetching up to 3 Shopping Lists per page.
            List<ShoppingList>? PageLists = new();
            for (int i = (Page - 1) * 3; i < Page * 3; i++)
            {
                if (UserLists.Count <= i) { break; } // Null Exception Preventation.

                PageLists.Add(UserLists.ElementAt(i));
            }

            return View(PageLists);

        }

        public async Task<IActionResult> DeleteShoppingList(string ListId, string ReturnAction)
        {
            var CurrentUser = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            var CurrentShoppingList = _context.ShoppingLists.First(sl => sl.Id == ListId && sl.UserId == CurrentUser.Id);

            //Favorite Lists are not deleted and can be retrieved from user profile.
            if (CurrentShoppingList.IsFavorited == true)
            {
                CurrentShoppingList.IsActive = false;
                _context.ShoppingLists.Update(CurrentShoppingList);
                await _context.SaveChangesAsync();
                return RedirectToAction(ReturnAction, new {Page = 1});
            }

            _context.ShoppingLists.Remove(CurrentShoppingList);
            await _context.SaveChangesAsync();

            return RedirectToAction(ReturnAction, new { Page = 1 });
        }

        //Functionality for adding lists to favorites.
        public async Task<IActionResult> SwitchIsFavorited(string ListId, bool OldStatus, int Page)
        {
            var CurrentUser = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            var ShoppingListContext = _context.ShoppingLists.First(sl => sl.Id == ListId && sl.UserId == CurrentUser.Id).IsFavorited = !OldStatus;

            await _context.SaveChangesAsync();

            return RedirectToAction("ActiveLists", new { Page });
        }
    }
}
