using System.Text.Json.Serialization;

namespace Movie.Model
{
    public class MovieResults
{
        [JsonPropertyName("results")]
        public MovieResults[] Results { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }
        [JsonPropertyName("vote_average")]
        public float VoteAverage { get; set; }
        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }
        [JsonPropertyName("overview")]
        public string Overview { get; set; }
        [JsonPropertyName("genre_ids")]
        public int[] GenreIds { get; set; }
        

    }
    public class MovieDetail
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public float VoteAverage { get; set; }
        public string ReleaseDate { get; set; }
        public string Overview { get; set; }
        public List<string> SimilarMovies { get; set; }
    }

}
