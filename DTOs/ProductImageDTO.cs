using Laptopy.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Laptopy.DTOs
{
    public class ProductImageDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
       
        public string Model { get; set; } = string.Empty;
       
        public int CategoryID { get; set; }
        [ValidateNever]
        public Category Category { get; set; } = null!;


    }
}
