﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DataAccess.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired();
            builder.Property(x => x.LastName)
                .IsRequired();
            builder.Property(x => x.Email)
                .IsRequired();
            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.Property(x => x.Username)
                .IsRequired();
            builder.HasIndex(x => x.Username)
                .IsUnique();
            builder.Property(x => x.PasswordHash)
                .IsRequired();
            builder.Property(x => x.PasswordSalt)
                .IsRequired();
        }
    }
}
