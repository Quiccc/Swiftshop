using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swiftshop.Database;
using Swiftshop.Models;

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

        public async Task<IActionResult> CreateProduct(string Name, string SubcategoryName, string ProductImage)
        {
            var ProductContext = _context.Products;
            var SubcategoryContext = _context.Subcategories;

            if (!ModelState.IsValid)
            {
                if (Name.IsNullOrEmpty())
                {
                    TempData["CreateErrorName"] = "Product name cannot be empty.";
                }

                if (ProductImage.IsNullOrEmpty())
                {
                    TempData["CreateErrorImage"] = "Product Image cannot be empty.";
                }

                if (ProductContext.Select(p => p.Name).ToList().Contains(Name))
                {
                    TempData["CreateErrorName"] = "Product name already exists.";
                }

                return RedirectToAction("ManageProducts", "Admin", new { SubcategoryId = 0 });
            }

            var ProductSubcategory = SubcategoryContext.First(sc => sc.Name == SubcategoryName);

            Product NewProduct = new()
            {
                Name = Name,
                SubcategoryId = ProductSubcategory.Id,
                Subcategory = ProductSubcategory,
                ProductImage = ProductImage
            };

            await _context.Products.AddAsync(NewProduct);
            _context.SaveChanges();

            return RedirectToAction("ManageProducts", "Admin", new { SubcategoryId = 0, Prefix = NewProduct.Name });
        }

        public async Task<IActionResult> DeleteProduct(string ProductId)
        {
            var ProductContext = _context.Products;
            var DeletedProduct = ProductContext.First(p => p.Id == ProductId);

            _context.Products.Remove(DeletedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin", new { SubcategoryId = 0, Prefix = DeletedProduct.Name });
        }

        public async Task<IActionResult> UpdateProductName(string ProductId, string Name)
        {
            var ProductContext = _context.Products;
            var UpdatedProduct = ProductContext.First(p => p.Id == ProductId);

            if (!ModelState.IsValid)
            {
                Dictionary<string, string> UpdateErrorName = new()
                {
                    {ProductId, "Product name cannot be empty." }
                };
                TempData["UpdateErrorName"] = UpdateErrorName;
                return RedirectToAction("ManageProducts", "Admin", new { SubcategoryId = 0, Prefix = UpdatedProduct.Name });
            }

            else if (ProductContext.Select(p => p.Name).ToList().Contains(Name))
            {
                Dictionary<string, string> UpdateErrorName = new()
                {
                    {ProductId, "Product name already exists." }
                };
                TempData["UpdateErrorName"] = UpdateErrorName;
                return RedirectToAction("ManageProducts", "Admin", new { SubcategoryId = 0, Prefix = UpdatedProduct.Name });
            }

            UpdatedProduct.Name = Name;

            _context.Products.Update(UpdatedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin", new { UpdatedProduct.SubcategoryId, Prefix = UpdatedProduct.Name });
        }

        public async Task<IActionResult> UpdateProductImage(string ProductId, string ProductImage)
        {
            var ProductContext = _context.Products;
            var UpdatedProduct = ProductContext.First(p => p.Id == ProductId);

            if (!ModelState.IsValid)
            {
                Dictionary<string, string> UpdateErrorName = new()
                {
                    {ProductId, "Product Image URL cannot be empty." }
                };
                TempData["UpdateErrorImage"] = UpdateErrorName;
                return RedirectToAction("ManageProducts", "Admin", new { SubcategoryId = 0, Prefix = UpdatedProduct.Name });
            }    

            UpdatedProduct.ProductImage = ProductImage;

            _context.Products.Update(UpdatedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin", new { UpdatedProduct.SubcategoryId, Prefix = UpdatedProduct.Name });
        }

        public async Task<IActionResult> UpdateProductSubcategory(string ProductId, string NewSubcategoryName)
        {
            var ProductContext = _context.Products;
            var SubcategoryContext = _context.Subcategories;

            var NewSubcategory = SubcategoryContext.First(sc => sc.Name == NewSubcategoryName);
            var UpdatedProduct = ProductContext.First(p => p.Id == ProductId);

            UpdatedProduct.Subcategory = NewSubcategory;
            UpdatedProduct.SubcategoryId = NewSubcategory.Id;

            _context.Products.Update(UpdatedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageProducts", "Admin", new { SubcategoryId = NewSubcategory.Id });
        }
    }
}
