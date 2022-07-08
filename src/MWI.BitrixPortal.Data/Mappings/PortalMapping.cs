using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MWI.BitrixPortal.Domain.Entities;
using MWI.Core.DomainObjects;

namespace MWI.BitrixPortal.Data.Mappings
{
    public class PortalMapping : IEntityTypeConfiguration<Portal>
    {
        public void Configure(EntityTypeBuilder<Portal> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.MemberId)
                .IsRequired()
                .HasColumnType($"varchar(40)");

            builder.Property(c => c.Domain)
                .IsRequired()
                .HasColumnType($"varchar(100)");

            builder.Property(c => c.Language)
                .IsRequired()
                .HasColumnType($"varchar(4)");

            builder.Property(c => c.ApplicationToken)
                .HasColumnType($"varchar(40)");

            builder.Property(c => c.BitrixAccountStatus)
                .IsRequired()
                .HasColumnType("char");

            builder.Property(c => c.AdminUserName)
                .HasColumnType($"varchar(100)");

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.Property(c => c.RefreshToken)
                .IsRequired()
                .HasColumnType($"varchar(40)");

            builder.Property(c => c.Active)
                .HasColumnType("bit");

            builder.Property(c => c.WizardMode)
                .HasColumnType("bit");

            builder.Property(c => c.PortalStatus)
                .IsRequired()
                .HasColumnType("char");

            builder.ToTable("Portals");
        }
    }
}
