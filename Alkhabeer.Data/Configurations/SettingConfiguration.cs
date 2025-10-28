using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alkhabeer.Data.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Key)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(x => x.Key).IsUnique();

            builder.Property(x => x.Group).HasMaxLength(100);
            builder.Property(x => x.Type).HasMaxLength(50);

            // ✅ Explicitly define timestamp type (avoids invalid default value)
            builder.Property(s => s.CreatedAt)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(s => s.UpdatedAt)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        }
    }
}

