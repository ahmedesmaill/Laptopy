using Laptopy.DTOs;
using Laptopy.Models;
using Laptopy.Repository;
using Laptopy.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Laptopy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImagesRepository imagesRepository;

        public ProductController(IProductRepository productRepository,IProductImagesRepository imagesRepository)
        {
            this.productRepository = productRepository;
            this.imagesRepository = imagesRepository;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, string? search = null)
        {
            if (page <= 0)
                page = 1;

            IQueryable<Product> products = productRepository.GetAll([e=>e.Category]);

            if (search != null && search.Length > 0)
            {
                search = search.TrimStart();
                search = search.TrimEnd();
                products = products.Where(e => e.Name.Contains(search));
            }

            products = products.Skip((page - 1) * 5).Take(5);

            if (products.Any())
            {
                return Ok(products.ToList());
            }

            return NotFound();
        }
        [HttpGet("Details")]
        public ActionResult Details(int categoryId)
        {
            var category = productRepository.GetOne(expression: e => e.Id == categoryId);
            if (category == null)
                return NotFound();
            return Ok(category);

        }
        [HttpPost("Create")]
        public ActionResult Create(Product product,List<IFormFile>images )
        {
            if (ModelState.IsValid)
            {
                foreach (var item in images)
                {


                    if (item.Length > 0) // 85896
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName); // "1.png"
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            item.CopyTo(stream);
                        }
                       
                        
                        
                            var productImage = new ProductImage
                            {
                                ImageUrl = fileName,
                                Product = product
                            };
                        product.ProductImages.Add(productImage);

                    }
                   

                }
                productRepository.Add(product);
                productRepository.Commit();
                return Ok(product);



               
            }
            return BadRequest(product);


            
        }
        // PUT: api/Product/{id}
       


    }


}

