using Library_management_system.Models.Database.Tables;
using Microsoft.EntityFrameworkCore;
namespace Library_management_system.Models.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options){}
        public DbSet<Users> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loans> Loans { get; set; }
    }
}
