using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Database;
using Swiftshop.Models;
using Swiftshop.Models.DTO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Swiftshop.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin, User")]
    public class FavoriteListsModel : PageModel
    {
        private readonly SwiftshopDbContext _context;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly UserManager<User> _userManager;
        public List<ShoppingList> FavoriteLists { get; set; } = new();
        public Dictionary<string, List<CompletedListContentDto>> FavoriteListContents { get; set; } = new();

        public FavoriteListsModel(SwiftshopDbContext context, IHttpContextAccessor httpAccessor, UserManager<User> userManager)
        {
            _context = context;
            _httpAccessor = httpAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var CurrentUser = await _userManager.FindByEmailAsync(_httpAccessor.HttpContext.User.Identity.Name);

            FavoriteLists = await _context.ShoppingLists
                .Where(sl => sl.UserId == CurrentUser.Id && sl.IsFavorited == true)
                .OrderBy(sl => sl.CreatedAt)
                .ToListAsync();

            foreach (var list in FavoriteLists)
            {
                FavoriteListContents.Add
                    (list.Id,
                    await _context.ShoppingListContents
                    .Where(slc => slc.ListId == list.Id)
                    .Join(
                        _context.Products,
                        slc => slc.ProductId,
                        p => p.Id,
                        (slc, p) => new CompletedListContentDto
                        { ListId = slc.ListId, ProductId = p.Id, ProductName = p.Name, ProductImage = p.ProductImage, Description = slc.Description, IsAcquired = slc.IsAcquired })
                    .ToListAsync());
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string ListId)
        {
            var CurrentUser = await _userManager.FindByEmailAsync(_httpAccessor.HttpContext.User.Identity.Name);

            if (ListId == "0")
            {
                var DeletedLists = await _context.ShoppingLists
                    .Where(sc => sc.UserId == CurrentUser.Id && sc.IsFavorited == true)
                    .ToListAsync();

                foreach(var list in DeletedLists)
                {
                    if(list.IsActive == true)
                    {
                        list.IsFavorited = false;
                        _context.ShoppingLists.Update(list);
                    }
                    else
                    {
                        _context.ShoppingLists.Remove(list);
                    }
                }

                await _context.SaveChangesAsync();
            }

            else
            {
                var DeletedList = await _context.ShoppingLists
                    .Where(sc => sc.UserId == CurrentUser.Id && sc.IsFavorited == true && sc.Id == ListId)
                    .FirstAsync();

                if(DeletedList.IsActive == true)
                {
                    DeletedList.IsFavorited = false;
                    _context.ShoppingLists.Update(DeletedList);
                }

                else
                {
                    _context.ShoppingLists.Remove(DeletedList);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("FavoriteLists");
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            var CurrentUser = await _userManager.FindByEmailAsync(_httpAccessor.HttpContext.User.Identity.Name);

            var UserFavoriteLists = await _context.ShoppingLists
                .Where(sl => sl.UserId == CurrentUser.Id && sl.IsFavorited == true)
                .ToListAsync();

            foreach(var list in UserFavoriteLists)
            {
                Debug.WriteLine("TESTTEST");
                if(list.IsActive == true)
                {
                    list.IsFavorited = false;
                    _context.ShoppingLists.Update(list);
                }
                else
                {
                    _context.ShoppingLists.Remove(list);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("FavoriteLists");
        }

        public async Task<IActionResult> OnPostAddAsync(string ListId)
        {
            var AddedList = await _context.ShoppingLists.FirstAsync(sl => sl.Id == ListId && sl.IsFavorited == true);

            if(AddedList.IsActive == true)
            {
                TempData["ErrorMessage"] = "Selected list is already active.";
                return RedirectToPage("FavoriteLists");
            }

            AddedList.IsActive = true;
            _context.ShoppingLists.Update(AddedList);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
