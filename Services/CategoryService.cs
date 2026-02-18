using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productApi.Data;
using productApi.Models;

namespace productApi.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            return categories;
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            if (category == null)
                return false;
            
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

