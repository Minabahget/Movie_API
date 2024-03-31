using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Movie_Api.DTO;
using Movie_Api.Models;
using Movie_Api.Repository;

namespace Movie_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MovieRepoService movieService;
        private readonly IMapper mapper;
        private readonly CategoryRepoService categoryService;

        // Constructor to inject dependencies
        public CategoryController(CategoryRepoService categoryService, MovieRepoService movieService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.movieService = movieService;
            this.mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get all categories
            var categories = categoryService.GetAll();

            if (categories is null || categories.Count == 0)
            {
                // If no categories found, return no content status
                return NoContent();
            }
            else
            {
                // Map categories to DTOs and return them
                var data = mapper.Map<List<Category>, List<CategoryWithMovie>>(categories);
                return Ok(data);
            }
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Get category by ID
            var category = categoryService.Details(id);
            if (category == null)
            {
                // If category not found, return no content status
                return NoContent();
            }
            else
            {
                // Map category to DTO and return it
                var data = mapper.Map<Category, CategoryWithMovie>(category);
                return Ok(data);
            }
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                // Add new category
                bool Result = categoryService.Add(category);
                if (Result)
                {
                    // Map category to DTO and return it
                    var data = mapper.Map<CategoryWithMovie>(category);
                    return Ok(data);
                }
                else
                    return BadRequest("Cant save in database");
            }
            // If model state is invalid, return bad request with errors
            return BadRequest(new { msg = "error!!", Errors = ModelState });
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Category category)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid category ID");
            }

            // Get existing category
            var existingCategory = categoryService.Details(id);
            if (existingCategory == null)
            {
                // If category not found, return not found status
                return NotFound("Category not found");
            }

            if (!ModelState.IsValid)
            {
                // If model state is invalid, return bad request with errors
                return BadRequest(ModelState);
            }

            // Update category properties
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.Movies = category.Movies;

            // Update category
            bool result = categoryService.Update(existingCategory);
            return result ? Ok(existingCategory) : BadRequest("Unable to save changes to the database");
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid category ID");
            }

            // Get category by ID
            var category = categoryService.Details(id);
            if (category == null)
            {
                // If category not found, return not found status
                return NotFound();
            }

            // Remove category
            var result = categoryService.Remove(category);
            //return result ? Ok("Category deleted successfully") : BadRequest("Unable to delete category");
            if(result == false)  
                return BadRequest("Unable to delete category");
      
            return Ok(result);
        }
    }
}
