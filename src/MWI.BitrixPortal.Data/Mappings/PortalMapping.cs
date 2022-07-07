using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MWI.BitrixPortal.Domain.Entities;
using MWI.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWI.BitrixPortal.Data.Mappings
{
    public class PortalMapping : IEntityTypeConfiguration<Portal>
    {
        public void Configure(EntityTypeBuilder<Portal> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.MemberId).IsRequired();

            builder.Property(c => c.Domain).IsRequired();

            builder.Property(c => c.Language).IsRequired().HasMaxLength(2);

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.ToTable("Portals");
        }
    }
}
