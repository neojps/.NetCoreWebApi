using Microsoft.EntityFrameworkCore;
using UserMngt.Core.Models;
using UserMngt.Data.Configurations;

namespace UserMngt.Data
{
    public class UserMngtDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserMngtDbContext(DbContextOptions<UserMngtDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new UserConfiguration());
        }
    }
}
