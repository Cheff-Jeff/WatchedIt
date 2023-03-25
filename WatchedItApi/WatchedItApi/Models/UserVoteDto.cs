namespace WatchedItApi.Models
{
    public class UserVoteDto
    {
        public int movielistId { get; set; }

        public int movieId { get; set; }

        public int userId { get; set; }

        public List<int>? movieIds { get; set; }

        public int voteCount { get; set; }
    }
}
