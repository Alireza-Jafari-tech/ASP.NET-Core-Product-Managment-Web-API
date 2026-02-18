using Microsoft.AspNetCore.Mvc;
using productApi.Data;
using productApi.Models;
using productApi.Services;

namespace productApi.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ProductService _productService;

        public ProductController(AppDbContext context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProductsAsync([FromBody] PaginationParams paginationParams)
        {
            var products = await _productService.GetProductsAsync(paginationParams);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("input id is invalid");
            }

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound($"product with id {id} wasnt found");
            }

            return Ok(product);
        }

        [HttpGet("byCategory")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryIdAsync([FromQuery] int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest("input id is invalid");

            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);

            if (products == null || !products.Any())
                return NotFound($"no products found for category with id {categoryId}");

            return Ok(products);
        }
        [HttpPost("filter")]
        public async Task<ActionResult<List<Product>>> FilterProducts([FromBody] ProductFilter filter)
        {
            if (!ModelState.IsValid)
                return BadRequest("invalid filter");

            var products = await _productService.FilterProductsAsync(filter);
            if (products == null || !products.Any())
                return NotFound("no products found matching the filter criteria");

            return Ok(products);
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<Product>>> SearchProduct([FromQuery] string searchTerm)
        {
            var filter = new ProductFilter()
            {
                Name = searchTerm
            };

            var products = await _productService.FilterProductsAsync(filter);

            if (products == null || !products.Any())
                return NotFound("no product was found");

            return Ok(products);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("input product is required");
            }

            bool isSuccessful = await _productService.AddProductAsync(product);
            if (isSuccessful)
                return Ok();

            return StatusCode(500, "something went wrong");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductAsync([FromQuery] int productId, [FromBody] Product updateProduct)
        {
            if (productId <= 0 || updateProduct == null)
            {
                return BadRequest("invalid input");
            }

            bool isSuccessful = await _productService.UpdateProductAsync(productId, updateProduct);
            if (isSuccessful)
                return Ok();

            return StatusCode(500, "something went wrong");
        }

        [HttpDelete("delete/")]
        public async Task<IActionResult> DeleteProductAsync([FromQuery] int productId)
        {
            bool isSuccessful = await _productService.DeleteProductAsync(productId);
            if (isSuccessful)
            {
                return Ok();
            }

            return StatusCode(500, "something went wrong");
        }
    }
}