﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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