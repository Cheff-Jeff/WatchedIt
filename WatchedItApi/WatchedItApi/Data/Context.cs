using Microsoft.EntityFrameworkCore;
using WatchedItApi.Models;

namespace WatchedItApi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<MovieList> MovieLists { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Movie> Movies { get; set; }
    }
}
