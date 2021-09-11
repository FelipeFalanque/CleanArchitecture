using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return View(categories);
        }
        
        public async Task<IActionResult> Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryDTO = await _categoryService.GetByIdAsync(id.Value);

            if (categoryDTO == null)
                return NotFound();

            return View(categoryDTO);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(category);

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryDTO = await _categoryService.GetByIdAsync(id.Value);

            if (categoryDTO == null)
                return NotFound();

            return View(categoryDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.RemoveAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
