
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WatchedItApi.Models
{
    public class MovieList
    {
        [Key]
        public int Id { get; set; }

        public string title { get; set; } = String.Empty;

        public int UserId { get; set; }

        public string voteDeadLine { get; set; } = String.Empty;

        public string watchDateTime { get; set; } = String.Empty;

        public ICollection<Movie>? Movies { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
