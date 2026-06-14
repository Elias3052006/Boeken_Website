using Microsoft.AspNetCore.Mvc;
using BisinessLogicLayer.Services;
using BisinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ViewLayer.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanService _loanService;

        
        public LoanController(LoanService loanService)
        {
            _loanService = loanService;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            string userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            int userId = int.Parse(userIdString);
            var myLoans = _loanService.GetLoansByUserId(userId);

            return View(myLoans);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int bookId)
        {
            string userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            int userId = int.Parse(userIdString);

            var dto = new LoanDTO
            {
                UserId = userId,
                BookId = bookId
            };

            
            bool success = _loanService.CreateLoan(dto);

            if (success)
            {
                return RedirectToAction("Index", "Loan");
            }

            
            ViewBag.ErrorMessage = "Het boek is helaas niet beschikbaar voor lening.";
            return View("Error");
        }
    }
}