using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swiftshop.Database;
using Swiftshop.DTOs;
using Swiftshop.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Swiftshop.Controllers
{
    public class LoginController : Controller
    {
        private readonly SwiftshopDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(SwiftshopDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserDto request)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(cu => cu.Email == request.Email);
            Debug.WriteLine("aerlmşkataersitgardklpsşifgkiaergiklpşaeklmşi");
            Debug.WriteLine(currentUser.Email + " " + currentUser.UserRole);

            if (currentUser.Email! == request.Email && BCrypt.Net.BCrypt.Verify(request.Password, currentUser.PasswordHash))
            {
                var jwt = CreateToken(currentUser.Email!, currentUser.UserRole!);
                return RedirectToAction("Index", "Home", new { jwt });
            }

            return RedirectToAction("Index", "LoginController", new {status = 1});
        }

        private string CreateToken(string Email, string UserRole)
        {
            List<Claim> tokenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.Role, UserRole)
            };

            Console.WriteLine(_configuration.GetSection("JWT.TokenKey"));

            //var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                //_configuration.GetSection("JWT.TokenKey").Value!));

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ASP.NET MVC Bootcamp 2023"));

            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: tokenClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: Credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
