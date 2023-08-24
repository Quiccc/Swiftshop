using Microsoft.AspNetCore.Mvc;

namespace Swiftshop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
