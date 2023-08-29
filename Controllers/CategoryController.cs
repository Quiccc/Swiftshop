using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Database;
using Swiftshop.Models;

namespace Swiftshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly SwiftshopDbContext _context;

        public CategoryController(SwiftshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateCategory(string Name)
        {
            var context = _context.Categories;

            if (!ModelState.IsValid)
            {
                TempData["CreateError"] = "Category name cannot be empty.";
                return RedirectToAction("ManageCategories", "Admin");
            }

            else if (context.Select(c => c.Name).Contains(Name))
            {
                TempData["CreateError"] = "Category name already exists.";
                return RedirectToAction("ManageCategories", "Admin");
            }

            Category NewCategory = new()
            {
                Name = Name
            };

            await _context.Categories.AddAsync(NewCategory);
            _context.SaveChanges();

            return RedirectToAction("ManageCategories", "Admin");
        }

        public async Task<IActionResult> DeleteCategory(string CategoryId)
        {
            var context = _context.Categories;
            var DeletedCategory = await context.FirstAsync(c => c.Id == CategoryId);

            _context.Categories.Remove(DeletedCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageCategories", "Admin");
        }

        public async Task<IActionResult> UpdateCategoryName(string CategoryId, string Name)
        {
            var context = _context.Categories;

            if (!ModelState.IsValid)
            {
                Dictionary<string, string> UpdateError = new()
                {
                    { CategoryId, "Category name cannot be null" }
                };

                TempData["UpdateError"] = UpdateError;
                return RedirectToAction("ManageCategories", "Admin");
            }

            else if (context.Select(c => c.Name).Contains(Name))
            {
                Dictionary<string, string> UpdateError = new()
                {
                    { CategoryId, "Category name already exists." }
                };
                TempData["UpdateError"] = UpdateError;
                return RedirectToAction("ManageCategories", "Admin");
            }

            var UpdatedCategory = context.First(c => c.Id == CategoryId);
            UpdatedCategory.Name = Name;

            _context.Categories.Update(UpdatedCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageCategories", "Admin");
        }
    }
}
