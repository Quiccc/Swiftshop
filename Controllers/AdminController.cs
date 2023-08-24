using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Database;
using Swiftshop.Models;

namespace Swiftshop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly SwiftshopDbContext _context;
        public AdminController(SwiftshopDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageCategories()
        {
            var context = _context.Categories;
            return View(await context.ToListAsync());
        }

        public async Task<IActionResult> DeleteCategory(string CategoryId)
        {
            var context = _context;
            var DeletedCategory = await context.Categories.FirstAsync(c => c.Id == CategoryId);

            context.Remove(DeletedCategory);
            context.SaveChanges();

            return RedirectToAction("ManageCategories");
        }

        public async Task<IActionResult> EditCategory(string CategoryId, string NewName)
        {
            var context = _context;
            var UpdatedCategory = await context.Categories.FirstAsync(c => c.Id == CategoryId);
            UpdatedCategory.Name = NewName;

            context.Categories.Update(UpdatedCategory);
            context.SaveChanges();

            return RedirectToAction("ManageCategories");
        }

        public async Task<IActionResult> CreateCategory(string NewName)
        {
            var context = _context;
            Category NewCategory = new()
            {
                Name = NewName
            };

            await context.Categories.AddAsync(NewCategory);
            context.SaveChanges();

            return RedirectToAction("ManageCategories");
        }

        public IActionResult ManageSubcategories()
        {
            return View();
        }

        public IActionResult ManageProducts()
        {
            return View();
        }
    }
}
