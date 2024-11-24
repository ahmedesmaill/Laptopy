using Laptopy.Models;

namespace Laptopy.DTOs
{
    public class ShoppingCart
    {
        public List<Cart> Carts { get; set; }
        public double TotalPrice { get; set; }
    }
}
