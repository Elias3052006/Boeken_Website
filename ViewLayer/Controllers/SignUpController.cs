using Microsoft.AspNetCore.Mvc;
using ViewLayer.Models;
using BisinessLogicLayer.DTOs;
using BisinessLogicLayer.Services;

namespace ViewLayer.Controllers
{
    public class SignUpController : Controller
    {
        private readonly UserService _userService;

        public SignUpController(UserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet]
        public IActionResult SignUp()
        {
            return View("~/Views/User/SignUp.cshtml");
        }

        
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/User/SignUp.cshtml", model);
            }

            
            var dto = new UserSignUpDto
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                PostCode = model.PostCode,
                HouseNumber = model.HouseNumber
            };

            bool success = _userService.SignUp(dto);

            if (success == false)
            {
                ModelState.AddModelError("", "Registratie mislukt. Gebruikersnaam of e-mail bestaat al.");
                return View("~/Views/User/SignUp.cshtml", model);
            }

            
            return RedirectToAction("Login", "Login");
        }
    }
}
