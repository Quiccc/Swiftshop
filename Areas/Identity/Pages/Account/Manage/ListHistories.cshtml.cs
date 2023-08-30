using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swiftshop.Database;
using Swiftshop.Models;
using Swiftshop.Models.DTO;

namespace Swiftshop.Areas.Identity.Pages.Account.Manage
{
    public class ListHistoriesModel : PageModel
    {
        private readonly SwiftshopDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public List<ShoppingListHistoryDto> UserListHistories { get; set; } = new();

        public ListHistoriesModel(SwiftshopDbContext context, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var CurrentUser = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            UserListHistories = await _context.ShoppingListHistories
               .Where(slh => slh.UserId == CurrentUser.Id)
               .Select(slh => new ShoppingListHistoryDto
               {
                   Id = slh.Id,
                   Name = slh.Name,
                   ProductNames = JsonConvert.DeserializeObject<List<string>>(slh.ListContents),
                   CreatedAt = slh.HistoryDate
               })
               .OrderByDescending(ulh => ulh.CreatedAt)
               .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string HistoryId)
        {
            var CurrentUser = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            if (HistoryId == "0")
            {
                var DeletedHistory = await _context.ShoppingListHistories
                    .Where(dh => dh.UserId == CurrentUser.Id)
                    .ToListAsync();

                _context.ShoppingListHistories.RemoveRange(DeletedHistory);
            }

            else
            {
                var DeletedHistory = await _context.ShoppingListHistories
                    .Where(dh => dh.UserId == CurrentUser.Id && dh.Id == HistoryId)
                    .FirstAsync();

                _context.ShoppingListHistories.Remove(DeletedHistory);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("ListHistories");
        }
    }
}
