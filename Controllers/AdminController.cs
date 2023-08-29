using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Database;
using Swiftshop.Models;

namespace Swiftshop.Controllers
{
    [Authorize(Roles = "Admin")]
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

            ViewBag.CategoryNames = CategoryContext.Select(c => c.Name).ToList();

            return View(await SubcategoryContext.ToListAsync());
        }

        public async Task<IActionResult> ManageProducts(string SubcategoryId, string Prefix)
        {
            if (Prefix == null) { Prefix = ""; }

            //Fetch all categories and subcategories for product filtering.
            var SubcategoryContext = await _context.Subcategories.ToListAsync();
            var CategoryContext = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Subcategories = SubcategoryContext;
            ViewBag.SubcategoryId = SubcategoryId;
            ViewBag.Categories = CategoryContext;
            ViewBag.Prefix = Prefix;

            //No product filtering
            if (SubcategoryId == "0")
            {
                var ProductContext = _context.Products.Where(p => p.Name.Contains(Prefix));
                return View(await ProductContext.ToListAsync());
            }

            //Returns products depending on selected subcategory and prefix.
            else
            {
                var ProductContext = _context.Products.Where(p => p.SubcategoryId == SubcategoryId && p.Name.Contains(Prefix));
                return View(await ProductContext.ToListAsync());
            }
        }
    }
}
