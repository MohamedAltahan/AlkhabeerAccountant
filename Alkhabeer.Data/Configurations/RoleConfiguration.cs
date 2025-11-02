using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alkhabeer.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(300);

            builder.Property(r => r.IsActive)
                .HasDefaultValue(true);

            // ✅ Explicitly define timestamp type (avoids invalid default value)
            builder.Property(s => s.CreatedAt)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(s => s.UpdatedAt)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

            // Relationships
            builder.HasMany(r => r.UserRoles)
                   .WithOne(ur => ur.Role)
                   .HasForeignKey(ur => ur.RoleId);

            builder.HasMany(r => r.RolePermissions)
                   .WithOne(rp => rp.Role)
                   .HasForeignKey(rp => rp.RoleId);
        }
    }
}
