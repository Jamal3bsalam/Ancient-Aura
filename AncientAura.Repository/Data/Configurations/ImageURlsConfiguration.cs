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
    public class ImageURlsConfiguration : IEntityTypeConfiguration<ImageURLs>
    {
        public void Configure(EntityTypeBuilder<ImageURLs> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn(1,1);
            builder.HasOne(I => I.AncientSites)
                   .WithMany(AS => AS.ImageURLs)
                   .HasForeignKey(I => I.AncientSitesId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
