using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productApi.Services;
using productApi.Data;
using productApi.Models;
using System.Runtime.CompilerServices;

namespace productApi.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly CategoryService _categoryService;
        
        public CategoryController(AppDbContext context, CategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            if (categories == null || !categories.Any())
                return NotFound("no categories found");

            return Ok(categories);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest("input is not valid");

            bool isSuccessful = await _categoryService.CreateCategoryAsync(category);
            if (isSuccessful)
                return Ok("new category was created successfully");

            return StatusCode(500, "something went wrong!");
        }
    }
}

