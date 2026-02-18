using System.ComponentModel.DataAnnotations;

namespace productApi.Models
{
  public class PaginationParams
  {
    [Required]
    public int PageSize { get; set; }
    [Required]
    public int PageNumber { get; set; }
  }
}