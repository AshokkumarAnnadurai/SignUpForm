using AngularApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
           // Console.WriteLine(options.ConnectionString);
        }

        public DbSet<users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<users>().ToTable("users");
        }
    }
}
