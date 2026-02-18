using productApi.Models;
using productApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace productApi.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync(PaginationParams paginationParams)
        {
            int pageNumber = paginationParams.PageNumber;
            int pageSize = paginationParams.PageSize;

            return await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            return product;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

            return products;
        }

        public async Task<List<Product>> FilterProductsAsync(ProductFilter filter)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(p => p.Name.Contains(filter.Name));
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (filter.MinPrice.HasValue && filter.MinPrice.Value >= 0)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            var products = await query.ToListAsync();
            return products;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            if (product == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return false;
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateProductAsync(int productId, Product updatedProduct)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);

            if (product == null)
            {
                return false;
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.CategoryId = updatedProduct.CategoryId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}