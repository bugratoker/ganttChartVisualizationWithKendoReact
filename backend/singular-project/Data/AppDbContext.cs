using Microsoft.EntityFrameworkCore;
using singular_project.Entities;

namespace singular_project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<CSV> CSVs { get; set; }

    }
}
