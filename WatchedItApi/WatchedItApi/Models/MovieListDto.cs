namespace WatchedItApi.Models
{
    public class MovieListDto
    {
        public int MovieListId { get; set; }

        public int movieId { get; set; }

        public int externId { get; set; }

        public string phoneNumber { get; set; } = string.Empty;

        public bool movie { get; set; }
    }
}
