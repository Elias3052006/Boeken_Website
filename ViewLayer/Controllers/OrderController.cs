using Microsoft.AspNetCore.Mvc;
using BisinessLogicLayer.Services;
using BisinessLogicLayer.DTOs;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly BookService _bookService;

        public OrderController(OrderService orderService, BookService bookService)
        {
            _orderService = orderService;
            _bookService = bookService;
        }

        
        public IActionResult Create(int id)
        {
            string sessionValue = HttpContext.Session.GetString("UserId");
            int userId;

            if (sessionValue is not null)
            {
                userId = int.Parse(sessionValue);
            }
            else
            {
                userId = 0;
            }

            if (userId == 0)
                return RedirectToAction("Login", "Login");

            var book = _bookService.GetBookById(id);

            if (book == null)
                return NotFound();

            var viewmodel = new OrderViewModel
            {
                UserId = userId,
                Books = new List<OrderBookItem>
                {
                    new OrderBookItem
                    {
                        BookId     = book.BookId,
                        Title      = book.Title,
                        Price      = book.Price,
                        Quantity   = 1,
                        IsSelected = true
                    }
                }
            };

            return View("~/Views/Order/Create.cshtml", viewmodel);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            string sessionValue = HttpContext.Session.GetString("UserId");
            int userId;

            if (sessionValue is not null)
            {
                userId = int.Parse(sessionValue);
            }
            else
            {
                userId = 0; 
            }

            if (userId == 0)
                return RedirectToAction("Login", "Login");

            var books = _bookService.GetAllBooks();

            var viewmodel = new OrderViewModel
            {
                UserId = userId,
                Books = books.Select(b => new OrderBookItem
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Price = b.Price
                }).ToList()
            };

            return View("~/Views/Order/Create.cshtml", viewmodel);
        }

        
        [HttpPost]
        public IActionResult Create(OrderViewModel viewmodel)
        {
            string sessionValue = HttpContext.Session.GetString("UserId");
            int userId;

            if (sessionValue is not null)
            {
                userId = int.Parse(sessionValue);
            }
            else
            {
                userId = 0;
            }

            if (userId == 0)
                return RedirectToAction("Login", "Login");

            var selectedBooks = viewmodel.Books
            .Where(b => b.IsSelected)
            .Where(b => b.Quantity > 0)
            .ToList();

            if (selectedBooks.Any() == false)
            {
                ModelState.AddModelError("", "Kies minimaal één boek.");
                return View("~/Views/Order/Create.cshtml", viewmodel);
            }

            var dto = new OrderDTO
            {
                UserId = userId,
                OrderRules = selectedBooks.Select(b => new OrderRuleDTO
                {
                    BookId = b.BookId,
                    Quantity = b.Quantity
                }).ToList()
            };

            bool success = _orderService.CreateOrder(dto);

            if (success == true)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Kon de bestelling niet plaatsen.");
            return View("~/Views/Order/Create.cshtml", viewmodel);
        }
    }
}
