using AuthNAndAuthZ.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthNAndAuthZ.DataContext
{
    public class SecureDbContext : DbContext
    {
        public SecureDbContext(DbContextOptions<SecureDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
