using Microsoft.EntityFrameworkCore;
using WEB_153503_Kiseleva.Domain.Entities;

namespace WEB_153503_Kiseleva.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
