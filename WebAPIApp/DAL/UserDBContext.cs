using Microsoft.EntityFrameworkCore;
using WebAPIApp.DAL.Configuration;
using WebAPIApp.Models;

namespace WebAPIApp.DAL
{
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDBContext(DbContextOptions<UserDBContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
