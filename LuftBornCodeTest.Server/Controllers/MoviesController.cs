using LuftBornCodeTest.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuftBornCodeTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;


        string _allowedExtentions = FileSettings.allowedExtentions;
        int _maxFileSize = FileSettings.maxFileSizeByte;


        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult>GetByIdAsync(int id)
        {
            var movie = await _moviesService.FindById(id);
            if(movie == null) { return NotFound(); }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto movieDto)
        {

            if (movieDto.Poster == null)
                return BadRequest("Error: Poster is required");
            #region ValidatePosterExtentionAndSize


            if (!_allowedExtentions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLowerInvariant()))
                return BadRequest($"Error: Only {_allowedExtentions} are allowed!");

            if (movieDto.Poster.Length > _maxFileSize)
                return BadRequest($"Maximum file size allowed is: {_maxFileSize} Byte");


            #endregion

            using var dataStream = new MemoryStream();
            await movieDto.Poster.CopyToAsync(dataStream);


            var movie = new Movie
            {
                Category = movieDto.Category,
                Description = movieDto.Description,
                Name = movieDto.Name,
                PublisherId = movieDto.PublisherId,
                Rate = movieDto.Rate,
                Poster = dataStream.ToArray()
                

            };

           await _moviesService.Add(movie);
            return Ok(movie);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm] MovieDto movieDto)
        {

            var movie = await _moviesService.FindById(id);
            if (movie == null) { return NotFound($"Error: Movie not found with ID: {id}");  }
            #region ValidatePosterExtentionAndSize


            if (movieDto.Poster != null)
            {

                if (!_allowedExtentions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLowerInvariant()))
                    return BadRequest($"Error: Only {_allowedExtentions} are allowed!");

                if (movieDto.Poster.Length > _maxFileSize)
                    return BadRequest($"Maximum file size allowed is: {_maxFileSize} Byte");

                using var dataStream = new MemoryStream();
                await movieDto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            #endregion


            _moviesService.Update(movie);
            return Ok(movie);

        }
          
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.FindById(id);
            if (movie == null) return NotFound($"No movie found with ID: {id}");
             _moviesService.Remove(movie);

            return Ok(movie);
        }
    }
}
