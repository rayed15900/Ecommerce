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

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CartConfigurations());
            modelBuilder.ApplyConfiguration(new CartItemConfigurations());
            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Products)
                .WithOne()
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Cart>()
                .HasOne(x => x.User)
                .WithOne(x => x.Cart)
                .HasForeignKey<Cart>(x => x.UserId);

            modelBuilder.Entity<Cart>()
                .HasMany(x => x.CartItems)
                .WithOne()
                .HasForeignKey(x => x.CartId)
                .IsRequired();

            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.Product)
                .WithOne(x => x.CartItem)
                .HasForeignKey<CartItem>(x => x.ProductId);
        }
    }
}
