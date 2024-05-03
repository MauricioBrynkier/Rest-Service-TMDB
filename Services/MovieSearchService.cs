using Movie.Model;
using System;
using System.Text.Json;

namespace Movie.Services
{

    public class MovieSearchService : IMovieSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public MovieSearchService(HttpClient httpClient, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MovieDetail> SearchMovieByTitle(string title)
        {
            var httpClient = _httpClientFactory.CreateClient("TMDB");

            using (var response = await httpClient.GetAsync($"search/movie?query={title}&include_adult=false&language=es-ES&page=1") )
            {
                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();
                var searchResult = JsonSerializer.Deserialize<MovieResults>(body);
                var firstResult = searchResult.Results.FirstOrDefault();
                
                if (firstResult == null)
                {
                    throw new Exception("No se encontraron peliculas con ese nombre");
                }

                string genre = string.Join("%2C", firstResult.GenreIds);

                var similarMovies =  await SearchSimilarMovies(genre, firstResult.Title);

                var movie = new MovieDetail
                {
                    Title = firstResult.Title,
                    Overview = firstResult.Overview,
                    ReleaseDate = firstResult.ReleaseDate,
                    OriginalTitle = firstResult.OriginalTitle,
                    VoteAverage = firstResult.VoteAverage,
                    SimilarMovies = similarMovies,
                };

                return movie;

            }
        }

        public async Task<List<string>> SearchSimilarMovies(string genre, string title)
        {
            var httpClient = _httpClientFactory.CreateClient("TMDB");
            using (var response = await httpClient.GetAsync($"discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=vote_count.desc&with_genres={genre}"))
            {
                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();
                var searchResult = JsonSerializer.Deserialize<MovieResults>(body);

                var similarMovies = searchResult.Results
                    .Where(m => m.Title != title)
                    .Take(5)
                    .Select(m => $"{m.Title} ({m.ReleaseDate?.Split('-')[0]})")
                    .ToList();

                return similarMovies;

            }
        }
    }
}
