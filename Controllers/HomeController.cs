using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swiftshop.Database;
using Swiftshop.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Swiftshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly SwiftshopDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(SwiftshopDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
        } 

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var Email = _contextAccessor.HttpContext?.User.Identity?.Name;
            var UserLists = await _context.Users.FirstAsync();
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}