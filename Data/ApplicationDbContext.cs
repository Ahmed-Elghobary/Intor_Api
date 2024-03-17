using Microsoft.EntityFrameworkCore;

namespace ApiBeginner.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext( DbContextOptions options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }

        DbSet<Product> Products { get; set; }
    }
}
