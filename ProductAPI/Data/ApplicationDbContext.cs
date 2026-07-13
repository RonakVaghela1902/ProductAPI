using Microsoft.EntityFrameworkCore;
using ProductAPI.Models.Entities;

namespace ProductAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
