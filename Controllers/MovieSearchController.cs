using Microsoft.AspNetCore.Mvc;
using Movie.Model;
using Movie.Services;

namespace MovieSearcher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieSearchController : ControllerBase
    {
        private readonly IMovieSearchService _movieSearchService;

        public MovieSearchController(IMovieSearchService movieSearchService)
        {
            _movieSearchService = movieSearchService ?? throw new ArgumentNullException(nameof(movieSearchService));
        }

        [HttpGet("title")]
        public async Task<IActionResult> SearchMovieByTitle(string title)
        {
            try
            {
                var movie = await _movieSearchService.SearchMovieByTitle(title);
                return Ok(movie);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
    
}
