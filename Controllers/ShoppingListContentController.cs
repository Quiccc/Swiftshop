using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swiftshop.Database;
using Swiftshop.Models;
using System.Diagnostics;

namespace Swiftshop.Controllers
{
    public class ShoppingListContentController : Controller
    {
        private readonly SwiftshopDbContext _context;

        public ShoppingListContentController(SwiftshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string ListId, string SubcategoryId)
        {
            var SubcategoryContext = _context.Subcategories.ToList();
            var CategoryContext = _context.Categories
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.ListId = ListId;
            ViewBag.SubcategoryId = SubcategoryId;
            ViewBag.Categories = CategoryContext;

            if (SubcategoryId == "0")
            {
                var ProductContext = _context.Products;
                return View(await ProductContext.ToListAsync());
            }

            else
            {
                var ProductContext = _context.Products.Where(p => p.SubcategoryId == SubcategoryId);
                return View(await ProductContext.ToListAsync());
            }
        }

        public async Task<IActionResult> AddProductToList(string ListId, string ProductId, string Description, string SubcategoryId)
        {
            var CurrentListContentContext = _context.ShoppingListContents
                .Where(clc => clc.ListId == ListId && clc.ProductId == ProductId)
                .ToList();

            if (!CurrentListContentContext.IsNullOrEmpty())
            {
                return RedirectToAction("Index", new { ListId, SubcategoryId });
            }

            var CurrentShoppingList = _context.ShoppingLists.Where(sl => sl.Id == ListId).First();
            var AddedProduct = _context.Products.Where(p => p.Id == ProductId).First();

            var ShoppingListContentContext = _context.ShoppingListContents;

            Debug.WriteLine("ListID " + ListId);
            Debug.WriteLine("ProductID" + ProductId);
            Debug.WriteLine("Description" + Description);

            ShoppingListContent NewListContent = new()
            {
                ListId = ListId,
                List = CurrentShoppingList,
                ProductId = ProductId,
                Product = AddedProduct,
                Description = Description
            };

            await _context.AddAsync(NewListContent);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { ListId, SubcategoryId });
        }
    }
}
