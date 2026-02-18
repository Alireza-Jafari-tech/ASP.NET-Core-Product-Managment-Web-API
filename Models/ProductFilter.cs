namespace productApi.Models
{
    public class ProductFilter
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}