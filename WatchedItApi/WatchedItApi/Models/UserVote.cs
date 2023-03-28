using System.Reflection.Metadata;

namespace WatchedItApi.Models
{
    public class UserVote
    {
        public int Id { get; set; }

        public int MovieListId { get; set; }

        public int userId { get; set; }

        public bool voted { get; set; }
    }
}
