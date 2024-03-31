using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie_Api.DTO;
using Movie_Api.Models;
using Movie_Api.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly CategoryRepoService categoryService;
        private readonly MovieRepoService movieService;

        // Constructor to inject dependencies
        public MovieController(MovieRepoService movieService, CategoryRepoService categoryService, IMapper mapper)
        {
            this.movieService = movieService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        // GET: api/Movie
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get all movies
            var movies = movieService.GetAll();
            if (movies is null || movies.Count == 0)
            {
                // If no movies found, return no content status
                return NoContent();
            }
            else
            {
                // Map movies to DTOs and return them
                var data = mapper.Map<List<Movie>, List<MovieWithCategory>>(movies);
                return Ok(data);
            }
        }

        // GET: api/Movie/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Get movie by ID
            var movie = movieService.Details(id);
            if (movie == null)
            {
                // If movie not found, return no content status
                return NoContent();
            }
            else
            {
                // Map movie to DTO and return it
                var data = mapper.Map<Movie, MovieWithCategory>(movie);
                return Ok(data);
            }
        }

        // POST: api/Movie
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddMovie([FromForm] MovieCreateModel model)
        {
            if (ModelState.IsValid)
            {
                // Add image
                string imagePath = await Utilities.Utility.AddImage(model);

                // Get categories
                List<Category> categories = new List<Category>();
                foreach (var categoryId in model.Categories)
                {
                    var category = categoryService.Details(categoryId);
                    if (category != null)
                    {
                        categories.Add(category);
                    }
                }

                // Create movie object
                Movie movie = new Movie()
                {
                    Description = model.Description,
                    Duration = model.Duration,
                    ImagePath = imagePath,
                    CreatedDate = model.CreatedDate,
                    Title = model.Title,
                    Rate = model.Rate,
                    Categories = categories
                };

                // Add movie
                bool result = movieService.Add(movie);

                return result ? Ok("Movie added successfully") : BadRequest("Unable to save in the database");
            }

            return BadRequest(new { msg = "Error!!", Errors = ModelState });
        }

        // PUT: api/Movie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MovieCreateModel model)
        {
            // Get existing movie
            var existingMovie = movieService.Details(id);
            if (ModelState.IsValid && existingMovie != null)
            {
                string imagepath=existingMovie.ImagePath;

                if (model.Image is not null)
                {
                    // Delete existing image
                    Utilities.Utility.DeleteImage(existingMovie.ImagePath);
                    // Add new image
                    imagepath = await Utilities.Utility.AddImage(model);
                    
                }             
               
                // Get categories
                List<Category> categories = new List<Category>();
                foreach (var categoryId in model.Categories)
                {
                    categories.Add(categoryService.Details(categoryId));
                }

                // Update movie properties
                existingMovie.Description = model.Description;
                existingMovie.Duration = model.Duration;
                existingMovie.ImagePath = imagepath;
                existingMovie.CreatedDate = model.CreatedDate;
                existingMovie.Title = model.Title;
                existingMovie.Rate = model.Rate;
                existingMovie.Categories = categories;

                // Update movie
                bool result = movieService.Update(existingMovie);
                return result ? Ok(mapper.Map<Movie, MovieWithCategory>(existingMovie)) : BadRequest("Unable to update in the database");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Movie/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Get movie by ID
            var movie = movieService.Details(id);

            if (movie == null)
            {
                // If movie not found, return not found status
                return NotFound();
            }
            else
            {
                // Delete movie image
                Utilities.Utility.DeleteImage(movie.ImagePath);

                // Remove movie
                bool result = movieService.Remove(movie);
                // return result ? Ok("Movie deleted successfully") : BadRequest("Unable to delete Movie");
                if (result == false)
                    return BadRequest("Unable to delete Movie");

                return Ok(result);
            }
        }
    }
}
