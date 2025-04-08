using DoAnCoSo.Data;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace DoAnCoSo.Controllers
{
    public class AccountController : Controller
    {
        private readonly DoAnCoSoContext _context;

        public AccountController(DoAnCoSoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User _userFromPage)
        {
            var _user = _context.User.Where(m=>m.UserName == _userFromPage.UserName && m.UserPassword == _userFromPage.UserPassword).FirstOrDefault();
            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim("FullName", _user.UserName),
                new Claim(ClaimTypes.Role, _user.UserRole),
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                 HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index","Account");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _userFromPage)
        {
            if (ModelState.IsValid)
            {
                var check = _context.User.FirstOrDefault(m=> m.UserEmail == _userFromPage.UserEmail);
                if (check == null)
                {
                    _context.ConfigureAwait(false);
                    _context.User.Add(_userFromPage);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = "Email already exists";
					TempData["error"] = "Email đã tồn tại! Vui lòng thử email khác";
					return View();
                }
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
