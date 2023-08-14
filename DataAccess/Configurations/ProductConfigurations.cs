using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.Price)
                .IsRequired();
            builder.Property(x => x.Description)
                .IsRequired(false);
            builder.Property(x => x.CategoryId)
                .IsRequired();
            builder.Property(x => x.InventoryId)
                .IsRequired();
            builder.Property(x => x.DiscountId)
                .IsRequired();
        }
    }
}
