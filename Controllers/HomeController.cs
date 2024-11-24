using Laptopy.Models;
using Laptopy.Repository;
using Laptopy.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Index(int page = 1, string? search = null)
        {
            if (page <= 0)
                page = 1;

            IQueryable<Product> products = productRepository.GetAll();

            if (search != null && search.Length > 0)
            {
                search = search.TrimStart();
                search = search.TrimEnd();
                products = products.Where(e => e.Name.Contains(search));
            }

            products = products.Skip((page - 1) * 5).Take(5);

            if (products.Any())
            {
                return Ok(products);
            }

            return NotFound();
        }
        [HttpGet("Details")]
        public IActionResult Details(int productId)
        {
            var product = productRepository.GetOne(expression:e=>e.Id==productId);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
