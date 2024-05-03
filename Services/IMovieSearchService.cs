using Movie.Model;

namespace Movie.Services
{
    public interface IMovieSearchService
    {
        Task<MovieDetail> SearchMovieByTitle(string title);
    }
}
