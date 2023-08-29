using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swiftshop.Database;
using Swiftshop.Models;

namespace Swiftshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubcategoryController : Controller
    {
        private readonly SwiftshopDbContext _context;

        public SubcategoryController(SwiftshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateSubcategory(string Name, string CategoryName)
        {
            var SubcategoryContext = _context.Subcategories;
            var CategoryContext = _context.Categories;

            if (!ModelState.IsValid)
            {
                TempData["CreateError"] = "Subcategory name cannot be null";
                return RedirectToAction("ManageSubcategories", "Admin");
            }

            else if (SubcategoryContext.Select(sc => sc.Name).ToList().Contains(Name))
            {
                TempData["CreateError"] = "Subcategory name already exists.";
                return RedirectToAction("ManageSubcategories", "Admin");
            }

            var NewCategory = CategoryContext.First(c => c.Name == CategoryName);

            Subcategory NewSubcategory = new()
            {
                Name = Name,
                CategoryId = NewCategory.Id,
                Category = NewCategory
            };

            await _context.AddAsync(NewSubcategory);
            _context.SaveChanges();

            return RedirectToAction("ManageSubcategories", "Admin");
        }

        public async Task<IActionResult> DeleteSubcategory(string SubcategoryId)
        {
            var context = _context.Subcategories;
            var DeletedSubcategory = context.First(sc => sc.Id == SubcategoryId);

            _context.Subcategories.Remove(DeletedSubcategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageSubcategories", "Admin");
        }

        public async Task<IActionResult> UpdateSubcategoryName(string SubcategoryId, string Name)
        {
            var context = _context.Subcategories;

            if (!ModelState.IsValid)
            {
                Dictionary<string, string> UpdateError = new()
                {
                    {SubcategoryId, "Subcategory name cannot be null."}
                };
                TempData["UpdateError"] = UpdateError;
                return RedirectToAction("ManageSubcategories", "Admin");
            }

            else if (context.Select(c => c.Name).ToList().Contains(Name))
            {
                Dictionary<string, string> UpdateError = new()
                {
                    {SubcategoryId, "Subcategory name already exists."}
                };
                TempData["UpdateError"] = UpdateError;
                return RedirectToAction("ManageSubcategories", "Admin");
            }

            var UpdatedSubcategory = context.First(sc => sc.Id == SubcategoryId);
            UpdatedSubcategory.Name = Name;

            _context.Subcategories.Update(UpdatedSubcategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageSubcategories", "Admin");
        }

        public async Task<IActionResult> UpdateSubcategoryCategory(string SubcategoryId, string NewCategoryName)
        {
            var SubcategoryContext = _context.Subcategories;
            var CategoryContext = _context.Categories;

            if (!CategoryContext.Select(c => c.Name).ToList().Contains(NewCategoryName))
            {
                return RedirectToAction("ManageSubcategories", "Admin");
            }

            var NewCategory = CategoryContext.First(c => c.Name == NewCategoryName);

            var UpdatedSubcategory = SubcategoryContext.First(sc => sc.Id == SubcategoryId);
            UpdatedSubcategory.Category = NewCategory;
            UpdatedSubcategory.CategoryId = NewCategory.Id;

            _context.Subcategories.Update(UpdatedSubcategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageSubcategories", "Admin");
        }
    }
}
