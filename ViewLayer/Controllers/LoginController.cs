using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BisinessLogicLayer.Services;
using BisinessLogicLayer.Interfaces;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authService;

        public LoginController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/User/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewmodel)
        {
            if (ModelState.IsValid is false)
            {
                return View("~/Views/User/Login.cshtml", viewmodel);
            }

            var account = _authService.Authenticate(viewmodel.Username, viewmodel.Password);

            if (account is null)
            {
                ModelState.AddModelError("", "Inloggen mislukt.");
                return View("~/Views/User/Login.cshtml", viewmodel);
            }

            HttpContext.Session.SetString("Username", account.Username);
            HttpContext.Session.SetString("UserRole", account.Role);
            HttpContext.Session.SetString("UserId", account.Id.ToString());

            if (account.IsEmployee())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}