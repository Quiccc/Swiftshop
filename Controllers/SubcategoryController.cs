using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swiftshop.Database;
using Swiftshop.Models;
using System.Diagnostics;

namespace Swiftshop.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly SwiftshopDbContext _context;

        public SubcategoryController(SwiftshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateSubcategory(string SubcategoryName, string CategoryName)
        {
            var SubcategoryContext = _context.Subcategories;
            var CategoryContext = _context.Categories;

            if (SubcategoryContext.Select(sc => sc.Name).ToList().Contains(SubcategoryName))
            {
                return RedirectToAction("ManageSubcategories", "Admin");
            }

            if(!CategoryContext.Select(c => c.Name).ToList().Contains(CategoryName))
            {
                return RedirectToAction("ManageSubcategories", "Admin");
            }

            var NewCategory = CategoryContext.First(c => c.Name == CategoryName);

            Subcategory NewSubcategory = new()
            {
                Name = SubcategoryName,
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

    public async Task<IActionResult> UpdateSubcategoryName(string SubcategoryId, string NewName)
    {
        var context = _context.Subcategories;

        if (context.Select(c => c.Name).ToList().Contains(NewName))
        {
            return RedirectToAction("ManageSubcategories", "Admin");
        }

        var UpdatedSubcategory = context.First(sc => sc.Id == SubcategoryId);
        UpdatedSubcategory.Name = NewName;

        _context.Subcategories.Update(UpdatedSubcategory);
        await _context.SaveChangesAsync();

        return RedirectToAction("ManageSubcategories", "Admin");
    }

    public async Task<IActionResult> UpdateSubcategoryCategory(string SubcategoryId, string NewCategoryName)
    {
        var SubcategoryContext = _context.Subcategories;
        var CategoryContext = _context.Categories;

            if(!CategoryContext.Select(c => c.Name).ToList().Contains(NewCategoryName))
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
