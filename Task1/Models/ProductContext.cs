using Microsoft.EntityFrameworkCore;


namespace Task1.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCreateInputModel> ProductsCIM { get; set; }
        public DbSet<ProductUpdateInputModel> ProductsUIM { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductCreateInputModel>().ToTable("Product");
            modelBuilder.Entity<ProductUpdateInputModel>().ToTable("Product");
        }
    }

}
