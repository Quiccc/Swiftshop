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

        public async Task<IActionResult> ManageSubcategories()
        {
            var SubcategoryContext = _context.Subcategories;
            var CategoryContext = _context.Categories.ToList();

            Dictionary<string, string> CategoryPairs = new();

            foreach(var category in CategoryContext)
            {
                CategoryPairs.Add(category.Id, category.Name);
            }

            ViewBag.CategoryPairs = CategoryPairs;

            return View(await SubcategoryContext.ToListAsync());
        }

        public IActionResult ManageProducts()
        {
            return View();
        }
    }
}
