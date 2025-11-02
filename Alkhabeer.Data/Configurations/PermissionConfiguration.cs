using Alkhabeer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alkhabeer.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Key)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.DisplayName)
                .HasMaxLength(200);
        }
    }
}
