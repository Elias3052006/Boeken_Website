using Microsoft.AspNetCore.Mvc;
using BisinessLogicLayer.Services;
using BisinessLogicLayer.DTOs;
using ViewLayer.Models;
using System;
using System.Collections.Generic;

namespace ViewLayer.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;

        public BookController(BookService bookService, CategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public IActionResult Details(int id)
        {
            try
            {
                var book = _bookService.GetBookById(id);

                var viewmodel = new BookDetailViewModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    Author = book.Author,
                    Stock = book.Stock,
                    IsAvailable = book.Is_Available
                };

                return View("~/Views/Book/Details.cshtml", viewmodel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewmodel = new BookViewModel
            {
                Categories = _categoryService.GetAllCategory()
            };

            return View("~/Views/Book/Create.cshtml", viewmodel);
        }

        [HttpPost]
        public IActionResult Create(BookViewModel viewmodel)
        {
            if (ModelState.IsValid == false)
            {
                viewmodel.Categories = _categoryService.GetAllCategory();
                return View("~/Views/Book/Create.cshtml", viewmodel);
            }

            var dto = new BookDTO
            {
                Title = viewmodel.Title,
                Description = viewmodel.Description,
                Price = viewmodel.Price,
                Stock = viewmodel.Stock,
                CategoryId = viewmodel.CategoryId,
                Author = viewmodel.Author
            };

            try
            {
                if (_bookService.AddBook(dto))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            viewmodel.Categories = _categoryService.GetAllCategory();
            return View("~/Views/Book/Create.cshtml", viewmodel);
        }

        [HttpPost]
        public IActionResult Update(int id, BookViewModel viewmodel)
        {
            if (ModelState.IsValid == false)
            {
                return View("Edit", viewmodel);
            }

            var dto = new BookDTO
            {
                Title = viewmodel.Title,
                Description = viewmodel.Description,
                Price = viewmodel.Price,
                Stock = viewmodel.Stock,
                CategoryId = viewmodel.CategoryId,
                Author = viewmodel.Author
            };

            try
            {
                _bookService.UpdateBook(id, dto);
                return RedirectToAction("Details", new { id = id });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return View("Edit", viewmodel);
        }
    }
}