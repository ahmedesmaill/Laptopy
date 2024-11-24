using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;

namespace Laptopy.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public string Model { get; set; } = string.Empty;
        [Required]
        public int CategoryID { get; set; }
        [ValidateNever]
        public Category Category { get; set; } = null!;
    }
}
