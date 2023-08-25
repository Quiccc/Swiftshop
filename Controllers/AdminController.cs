using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swiftshop.Database;
using Swiftshop.Models;
using System.Diagnostics;

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

        public async Task<IActionResult> ManageProducts(string SubcategoryId)
        {
            List<Product> ProductContext;
            if (SubcategoryId != null)
            {
                ProductContext = await _context.Products
                .Where(p => p.SubcategoryId == SubcategoryId).ToListAsync();
            }
            else
            {
                ProductContext = await _context.Products.ToListAsync();
            }

            var SubcategoryContext = _context.Subcategories
                .OrderBy(o => o.Name)
                .ToList();

            var CategoryContext = _context.Categories
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Categories = CategoryContext;
            ViewBag.Subcategories = SubcategoryContext;

            return View(ProductContext);
        }
    }
}
