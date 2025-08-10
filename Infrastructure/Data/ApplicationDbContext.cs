using Microsoft.EntityFrameworkCore;
using Vidya.Domain.Entities;

namespace Vidya.Infrastructure.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<College> colleges { get; set; }

        public DbSet<MenuList> MenuLists { get; set; }
        public DbSet<MenuRights> MenuRights { get; set; }

        
    }

}
