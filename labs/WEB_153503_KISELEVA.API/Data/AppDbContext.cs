using System;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.Domain.Entities;

namespace WEB_153503_KISELEVA.API.Data
{
	public class AppDbContext : DbContext
    {
		public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

