using DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Context
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());

            modelBuilder.Entity<Category>()
                .HasMany(u => u.Products)
                .WithOne()
                .HasForeignKey(s => s.CategoryId)
                .IsRequired();
        }
    }
}
