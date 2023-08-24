using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Database;
using Swiftshop.Models;

namespace Swiftshop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly SwiftshopDbContext _context;

        public CategoryController(SwiftshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DeleteCategory(string CategoryId)
        {
            var context = _context.Categories;
            var DeletedCategory = await context.FirstAsync(c => c.Id == CategoryId);

            _context.Categories.Remove(DeletedCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageCategories", "Admin");
        }

        public async Task<IActionResult> UpdateCategoryName(string CategoryId, string NewName)
        {
            var context = _context.Categories;

            if (context.Select(c => c.Name).ToList().Contains(NewName))
            {
                return RedirectToAction("ManageCategories", "Admin");
            }

            var UpdatedCategory =  context.First(c => c.Id == CategoryId);
            UpdatedCategory.Name = NewName;

            _context.Categories.Update(UpdatedCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageCategories", "Admin");
        }

        public async Task<IActionResult> CreateCategory(string NewName)
        {
            var context = _context.Categories;
            Category NewCategory = new()
            {
                Name = NewName
            };

            if (context.Select(c => c.Name).ToList().Contains(NewName))
            {
                return RedirectToAction("ManageCategories", "Admin");
            }

            await _context.Categories.AddAsync(NewCategory);
            _context.SaveChanges();

            return RedirectToAction("ManageCategories", "Admin");
        }
    }
}
