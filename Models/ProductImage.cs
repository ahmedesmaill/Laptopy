using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Laptopy.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [ValidateNever]
        public Product Product { get; set; }
    }
}
