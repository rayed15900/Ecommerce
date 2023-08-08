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
            modelBuilder.ApplyConfiguration(new DiscountConfigurations());
            modelBuilder.ApplyConfiguration(new InventoryConfigurations());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new OrderItemConfigurations());
            modelBuilder.ApplyConfiguration(new PaymentConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new ShippingDetailConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());

            // Category - Product (one-to-many)
            modelBuilder.Entity<Category>()
                .HasMany(x => x.Products)
                .WithOne()
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            // Product - Inventory (one-to-one)
            modelBuilder.Entity<Product>()
                .HasOne(x => x.Inventory)
                .WithOne(x => x.Product)
                .HasForeignKey<Product>(x => x.InventoryId)
                .IsRequired();

            // Discount - Product (one-to-many)
            modelBuilder.Entity<Discount>()
                .HasMany(x => x.Products)
                .WithOne()
                .HasForeignKey(x => x.DiscountId)
                .IsRequired();

            // Product - CartItem (one-to-one)
            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.Product)
                .WithOne(x => x.CartItem)
                .HasForeignKey<CartItem>(x => x.ProductId)
                .IsRequired();

            // Product - OrderItem (one-to-one)
            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Product)
                .WithOne(x => x.OrderItem)
                .HasForeignKey<OrderItem>(x => x.ProductId)
                .IsRequired();

            // Cart - CartItem (one-to-many)
            modelBuilder.Entity<Cart>()
                .HasMany(x => x.CartItems)
                .WithOne()
                .HasForeignKey(x => x.CartId)
                .IsRequired();

            // Cart - User (one-to-one)
            //modelBuilder.Entity<Cart>()
            //    .HasOne(x => x.User)
            //    .WithOne(x => x.Cart)
            //    .HasForeignKey<Cart>(x => x.UserId)
            //    .IsRequired();

            // Order - OrderItem (one-to-many)
            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .IsRequired();

            // Order - ShippingDetail (one-to-one)
            modelBuilder.Entity<Order>()
                .HasOne(x => x.ShippingDetail)
                .WithOne(x => x.Order)
                .HasForeignKey<Order>(x => x.ShippingDetailId)
                .IsRequired();

            // Order - Payment (one-to-one)
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Payment)
                .WithOne(x => x.Order)
                .HasForeignKey<Order>(x => x.PaymentId)
                .IsRequired();

            // Order - User (one-to-one)
            modelBuilder.Entity<Order>()
                .HasOne(x => x.User)
                .WithOne(x => x.Order)
                .HasForeignKey<Order>(x => x.UserId)
                .IsRequired();
        }
    }
}
