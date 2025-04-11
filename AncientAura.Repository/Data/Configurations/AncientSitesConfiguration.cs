using AncientAura.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations
{
    public class AncientSitesConfiguration : IEntityTypeConfiguration<AncientSites>
    {
        public void Configure(EntityTypeBuilder<AncientSites> builder)
        {
            builder.HasKey(AS => AS.Id);
            builder.Property(AS => AS.Id).UseIdentityColumn(1,1);

            builder.HasMany(AS => AS.Reviews)
                   .WithOne(R => R.AncientSites)
                   .HasForeignKey(R => R.AncientSitesId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
