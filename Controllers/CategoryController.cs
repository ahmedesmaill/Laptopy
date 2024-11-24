using Laptopy.Models;
using Laptopy.Repository.IRepository;
using Laptopy.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{SD.adminRole}")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet("Index")]
        public ActionResult Index(int page = 1, string? search = null)
        {
            if (page <= 0)
                page = 1;

            IQueryable<Category> categories = categoryRepository.GetAll();

            if (search != null && search.Length > 0)
            {
                search = search.TrimStart();
                search = search.TrimEnd();
                categories = categories.Where(e => e.Name.Contains(search));
            }

            categories = categories.Skip((page - 1) * 5).Take(5);

            if (categories.Any())
            {
                return Ok(categories.ToList());
            }

            return NotFound();
        }
        [HttpGet("Details")]
        public ActionResult Details(int categoryId)
        {
            var category = categoryRepository.GetOne(expression: e => e.Id == categoryId);
            if (category == null)
                return NotFound();
            return Ok(category);

        }
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                categoryRepository.Commit();
                return Created($"{Request.Scheme}://{Request.Host}/api/Category/Details?categoryId={category.Id}", category);
            }
            return BadRequest(category);
           
        }
        
        [HttpPut("Edit")]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                categoryRepository.Commit();
                return Created($"{Request.Scheme}://{Request.Host}/api/Category/Details?categoryId={category.Id}", category);

            }
            return BadRequest(category);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int categoryId)
        {
            var category = categoryRepository.GetOne(expression: e => e.Id == categoryId);
            if (category != null)
            {
                categoryRepository.Delete(category);
                categoryRepository.Commit();

                return Ok();
            }

            return NotFound();
        }


    }
}
