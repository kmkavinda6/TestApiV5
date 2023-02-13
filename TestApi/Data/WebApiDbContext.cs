using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TestApi.Models.Item> Item { get; set; }
        public DbSet<TestApi.Models.User> User { get; set; }
        public DbSet<TestApi.Models.Order> Order { get; set; }
        public DbSet<TestApi.Models.Stock> Stock { get; set; }
        public DbSet<TestApi.Models.Store> Store { get; set; }


       

    }
}
