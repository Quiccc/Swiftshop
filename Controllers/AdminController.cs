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

        public async Task<IActionResult> ManageCategories(string Prefix)
        {
            if (Prefix == null) { Prefix = ""; }
            var Categories = await _context.Categories
                .Where(c => c.Name.Contains(Prefix))
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Categories = Categories;
            ViewBag.Prefix = Prefix;

            return View();
        }

        public async Task<IActionResult> ManageSubcategories(string CategoryId, string Prefix)
        {
            if (Prefix == null) { Prefix = ""; }

            var Categories = _context.Categories
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Categories = Categories;
            ViewBag.CategoryId = CategoryId;
            ViewBag.Prefix = Prefix;

            if (CategoryId == "0")
            {
                var Subcategories = await _context.Subcategories
                    .Where(sc => sc.Name.Contains(Prefix))
                    .OrderBy(sc => sc.Category.Name)
                    .ThenBy(sc => sc.Name)
                    .ToListAsync();

                ViewBag.Subcategories = Subcategories;
                return View();
            }
            else
            {
                var Subcategories = await _context.Subcategories
                    .Where(sc => sc.Name.Contains(Prefix) && sc.CategoryId == CategoryId)
                    .OrderBy(sc => sc.Category.Name)
                    .ThenBy(sc => sc.Name)
                    .ToListAsync();

                ViewBag.Subcategories = Subcategories;
                return View();
            }
        }

        public async Task<IActionResult> ManageProducts(string SubcategoryId, string Prefix)
        {
            if (Prefix == null) { Prefix = ""; }

            //Fetch all categories and subcategories for product filtering.
            var SubcategoryContext = await _context.Subcategories
                .OrderBy(sc => sc.Name)
                .ToListAsync();

            var CategoryContext = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            foreach(var category in CategoryContext)
            {
                category.Subcategories = SubcategoryContext.Where(sc => sc.CategoryId == category.Id).ToList();
            }

            ViewBag.Subcategories = SubcategoryContext;
            ViewBag.SubcategoryId = SubcategoryId;
            ViewBag.Categories = CategoryContext;
            ViewBag.Prefix = Prefix;

            //No product filtering
            if (SubcategoryId == "0")
            {
                var ProductContext = await _context.Products
                    .Where(p => p.Name.Contains(Prefix))
                    .OrderBy(p => p.Subcategory.Name)
                    .ThenBy(p => p.Name)
                    .ToListAsync();

                ViewBag.Products = ProductContext;
                return View();
            }

            //Returns products depending on selected subcategory and prefix.
            else
            {
                var ProductContext = await _context.Products
                    .Where(p => p.SubcategoryId == SubcategoryId && p.Name.Contains(Prefix))
                    .OrderBy(p => p.Subcategory.Name)
                    .ThenBy(p => p.Name)
                    .ToListAsync();
                ViewBag.Products = ProductContext;
                return View();
            }
        }
    }
}
