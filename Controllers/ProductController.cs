using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swiftshop.Database;
using Swiftshop.Models;
using System.Diagnostics;

namespace Swiftshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly SwiftshopDbContext _context;

        public ProductController(SwiftshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateProduct(string ProductName, string SubcategoryName, string ProductImage)
        {
            var ProductContext = _context.Products;
            var SubcategoryContext = _context.Subcategories;

            if (ProductContext.Select(p => p.Name).ToList().Contains(ProductName))
            {
                return RedirectToAction("ManageProducts", "Admin");
            }

            var ProductSubcategory = SubcategoryContext.First(sc => sc.Name == SubcategoryName);

            Product NewProduct = new()
            {
                Name = ProductName,
                SubcategoryId = ProductSubcategory.Id,
                Subcategory = ProductSubcategory,
                ProductImage = ProductImage
            };

            await _context.Products.AddAsync(NewProduct);
            _context.SaveChanges();

            return RedirectToAction("ManageProducts", "Admin");
        }

        public async Task<IActionResult> DeleteProduct(string ProductId)
        {
            var ProductContext = _context.Products;
            var DeletedProduct = ProductContext.First(p => p.Id == ProductId);

            _context.Products.Remove(DeletedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin");
        }

        public async Task<IActionResult> UpdateProductName(string ProductId, string NewName)
        {
            var ProductContext = _context.Products;

            if (ProductContext.Select(p => p.Name).ToList().Contains(NewName))
            {
                return RedirectToAction("ManageProducts", "Admin");
            }

            var UpdatedProduct = ProductContext.First(p => p.Id == ProductId);
            UpdatedProduct.Name = NewName;

            _context.Products.Update(UpdatedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin");
        }

        public async Task<IActionResult> UpdateProductImage(string ProductId, string NewImage)
        {
            var ProductContext = _context.Products;
            var UpdatedProduct = ProductContext.First(p => p.Id == ProductId);

            UpdatedProduct.ProductImage = NewImage;

            _context.Products.Update(UpdatedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin");
        }

        public async Task<IActionResult> UpdateProductSubcategory(string ProductId, string NewSubcategoryName)
        {
            var ProductContext = _context.Products;
            var SubcategoryContext = _context.Subcategories;

            if (!SubcategoryContext.Select(sc => sc.Name).ToList().Contains(NewSubcategoryName))
            {
                return RedirectToAction("ManageProducts", "Admin");
            }

            var NewSubcategory = SubcategoryContext.First(sc => sc.Name == NewSubcategoryName);
            var UpdatedProduct = ProductContext.First(p => p.Id == ProductId);

            UpdatedProduct.Subcategory = NewSubcategory;
            UpdatedProduct.SubcategoryId = NewSubcategory.Id;

            _context.Products.Update(UpdatedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin");
        }
    }
}
