using Microsoft.EntityFrameworkCore;
using WatchedItApi.Models;

namespace WatchedItApi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
