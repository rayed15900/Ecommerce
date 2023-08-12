using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ShippingDetailConfigurations : IEntityTypeConfiguration<ShippingDetail>
    {
        public void Configure(EntityTypeBuilder<ShippingDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Country)
                .IsRequired();
            builder.Property(x => x.City)
                .IsRequired();
            builder.Property(x => x.Address)
                .IsRequired();
            builder.Property(x => x.Phone)
                .IsRequired();
        }
    }
}
