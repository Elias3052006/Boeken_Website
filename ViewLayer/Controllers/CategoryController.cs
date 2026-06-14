using Microsoft.AspNetCore.Mvc;
using BisinessLogicLayer.Services;
using BisinessLogicLayer.DTOs;
using ViewLayer.Models;
using System;

namespace ViewLayer.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Category/Create.cshtml", new CategoryViewModel());
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel viewmodel)
        {
            if (ModelState.IsValid == false)
            {
                return View("~/Views/Category/Create.cshtml", viewmodel);
            }

            var dto = new CategoryDTO { CategoryName = viewmodel.CategoryName };

            try
            {
                _categoryService.AddCategory(dto);
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Er is een onverwachte fout opgetreden.");
            }

            return View("~/Views/Category/Create.cshtml", viewmodel);
        }

        [HttpPost]
        public IActionResult Update(int id, CategoryViewModel viewmodel)
        {
            if (ModelState.IsValid == false)
            {
                return View("Edit", viewmodel);
            }

            var dto = new CategoryDTO { CategoryName = viewmodel.CategoryName };

            try
            {
                bool success = _categoryService.UpdateCategory(id, dto);
                if (success == false) return NotFound();

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Er is een onverwachte fout opgetreden.");
            }

            return View("Edit", viewmodel);
        }
    }
}