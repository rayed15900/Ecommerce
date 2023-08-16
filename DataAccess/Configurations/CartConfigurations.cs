using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalAmount)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired(false);
            builder.Property(x => x.IpAddress)
                .IsRequired(false);
            builder.Property(x => x.IsGuest)
                .IsRequired();
        }
    }
}
