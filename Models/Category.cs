using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Laptopy.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Minimum length must be 3")]
        [MaxLength(50, ErrorMessage = "Maximum length must be 50")]
        public string Name { get; set; } = string.Empty;

        //[ValidateNever]
        //public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
    }
}
