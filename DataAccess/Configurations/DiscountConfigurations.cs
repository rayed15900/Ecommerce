﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Configurations
{
    public class DiscountConfigurations : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.Percent)
                .IsRequired();
            builder.Property(x => x.Active)
                .IsRequired();
        }
    }
}
