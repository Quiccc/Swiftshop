using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swiftshop.Database;
using Swiftshop.Models;
using Swiftshop.Models.DTO;

namespace Swiftshop.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ShoppingListContentController : Controller
    {
        private readonly SwiftshopDbContext _context;

        public ShoppingListContentController(SwiftshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string ListId, string SubcategoryId)
        {
            //Fetch all categories and subcategories for product filtering.
            var SubcategoryContext = _context.Subcategories.ToList();
            var CategoryContext = _context.Categories
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.ListId = ListId;
            ViewBag.SubcategoryId = SubcategoryId;
            ViewBag.Categories = CategoryContext;

            //No product filtering
            if (SubcategoryId == "0")
            {
                var ProductContext = _context.Products;
                return View(await ProductContext.ToListAsync());
            }

            //Returns products depending on selected subcategory.
            else
            {
                var ProductContext = _context.Products.Where(p => p.SubcategoryId == SubcategoryId);
                return View(await ProductContext.ToListAsync());
            }
        }

        public async Task<IActionResult> AddProductToList(string ListId, string ProductId, string Description, string SubcategoryId)
        {
            var CurrentListContentContext = _context.ShoppingListContents
                .Where(slc => slc.ListId == ListId && slc.ProductId == ProductId)
                .ToList();

            if (!CurrentListContentContext.IsNullOrEmpty())
            {
                return RedirectToAction("Index", new { ListId, SubcategoryId });
            }

            var CurrentShoppingList = _context.ShoppingLists.Where(sl => sl.Id == ListId).First();
            var AddedProduct = _context.Products.Where(p => p.Id == ProductId).First();

            var ShoppingListContentContext = _context.ShoppingListContents;

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

        public async Task<IActionResult> ActiveListSummary(string ListId)
        {
            //Contents of an active shopping list.
            var ListContent = await _context.ShoppingListContents
                 .Where(slc => slc.ListId == ListId)
                 .Join(
                 _context.Products,
                 slc => slc.ProductId,
                 p => p.Id,
                 (slc, p) => new ActiveListContentDto
                 { ListId = slc.ListId, ProductId = p.Id, ProductName = p.Name, ProductImage = p.ProductImage, Description = slc.Description })
                 .ToListAsync();

            return View(ListContent);
        }

        public async Task<IActionResult> CompletedListSummary(string ListId)
        {
            //Contents of an completed shopping list.
            var ListContent = await _context.ShoppingListContents
                .Where(slc => slc.ListId == ListId)
                .Join(
                _context.Products,
                slc => slc.ProductId,
                p => p.Id,
                (slc, p) => new CompletedListContentDto
                { ListId = slc.ListId, ProductId = p.Id, ProductName = p.Name, ProductImage = p.ProductImage, Description = slc.Description, IsAcquired = slc.IsAcquired })
                .ToListAsync();

            return View(ListContent);
        }

        public async Task<IActionResult> SwitchIsAcquired(string ListId, string ProductId)
        {
            var AcquiredProduct = _context.ShoppingListContents.First(slc => slc.ListId == ListId && slc.ProductId == ProductId);
            AcquiredProduct.IsAcquired = true;

            _context.Update(AcquiredProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("CompletedListSummary", new { ListId });
        }

        public async Task<IActionResult> UpdateDescription(string ListId, string ProductId, string Description)
        {
            var UpdatedProduct = _context.ShoppingListContents.First(slc => slc.ListId == ListId && slc.ProductId == ProductId);

            if (UpdatedProduct.Description != null && UpdatedProduct.Description.Equals(Description))
            {
                return RedirectToAction("ActiveListSummary", new { ListId });
            }

            UpdatedProduct.Description = Description;
            _context.Update(UpdatedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ActiveListSummary", new { ListId });
        }

        public async Task<IActionResult> DeleteListProduct(string ListId, string ProductId)
        {
            var DeletedProduct = _context.ShoppingListContents.First(slc => slc.ListId == ListId && slc.ProductId == ProductId);
            _context.Remove(DeletedProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("ActiveListSummary", new { ListId });
        }

        public async Task<IActionResult> StartShopping(string ListId)
        {
            var ShoppingListContext = _context.ShoppingLists.First(sl => sl.Id == ListId).IsStarted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("CompletedLists", "ShoppingList", new { Page = 1 });
        }

        public async Task<IActionResult> CompleteShopping(string ListId)
        {
            //Get Acquired Products as list
            var AcquiredListContents = _context.ShoppingListContents.Where(slc => slc.ListId == ListId && slc.IsAcquired == true).ToList();

            //Change flag to finish shopping process.
            var CurrentList = _context.ShoppingLists.Where(sl => sl.Id == ListId).First();
            CurrentList.IsStarted = false;

            //Create a history of the completed list.
            ShoppingListHistory ListHistory = new()
            {
                Name = CurrentList.Name,
                ListContents = JsonConvert.SerializeObject(AcquiredListContents, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
                UserId = CurrentList.UserId,
                User = CurrentList.User
            };
            _context.ShoppingListHistories.Add(ListHistory);

            //Section for favorited lists. Acquired list products does not get removed.
            if (CurrentList.IsFavorited == true)
            {
                foreach (var content in AcquiredListContents)
                {
                    content.IsAcquired = false;
                    _context.ShoppingListContents.Update(content);
                }
            }

            //Section for regular lists. Acquired list products are removed.
            else
            {
                foreach (var content in AcquiredListContents)
                {
                    _context.ShoppingListContents.Remove(content);
                }
            }

            _context.ShoppingLists.Update(CurrentList);
            await _context.SaveChangesAsync();

            return RedirectToAction("ActiveLists", "ShoppingList", new { Page = 1 });
        }
    }
}
