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
        public DbSet<TestApi.Models.SalesRep> SalesReps { get; set; }
        public DbSet<TestApi.Models.Admin> Admins { get; set; }
        public DbSet<TestApi.Models.Manager> Managers { get; set; }
        public DbSet<TestApi.Models.Order> Order { get; set; }
        public DbSet<TestApi.Models.Stock> Stock { get; set; }
        public DbSet<TestApi.Models.Store> Store { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.totalAmount)
                .HasColumnType("decimal(18, 2)");
        }

    }
}
