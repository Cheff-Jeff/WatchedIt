namespace WatchedItApi.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public int phoneNumber { get; set; }

        public ICollection<MovieList>? MovieList { get; set; }
    }
}
