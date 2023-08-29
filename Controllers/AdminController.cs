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
            var Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Categories = Categories;

            return View();
        }

        public async Task<IActionResult> ManageSubcategories()
        {
            var Subcategories = await _context.Subcategories.ToListAsync();
            var CategoryContext = _context.Categories.ToList();

            ViewBag.Subcategories = Subcategories;
            ViewBag.CategoryNames = CategoryContext.Select(c => c.Name).ToList();

            return View();
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
                var ProductContext = await _context.Products.Where(p => p.Name.Contains(Prefix)).ToListAsync();
                ViewBag.Products = ProductContext;
                return View();
            }

            //Returns products depending on selected subcategory and prefix.
            else
            {
                var ProductContext = await _context.Products.Where(p => p.SubcategoryId == SubcategoryId && p.Name.Contains(Prefix)).ToListAsync();
                ViewBag.Products = ProductContext;
                return View();
            }
        }
    }
}
