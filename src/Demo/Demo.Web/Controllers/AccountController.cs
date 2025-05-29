using Demo.Web.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            return View(model);
        }
        [Authorize]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
