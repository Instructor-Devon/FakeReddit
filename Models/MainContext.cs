using Microsoft.EntityFrameworkCore;
namespace FakeReddit.Models
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users {get;set;}
        public DbSet<Post> Posts {get;set;}
        public DbSet<Category> Categories {get;set;}
        public DbSet<Vote> Votes {get;set;}
        
    }

}