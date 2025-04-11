using AncientAura.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations
{
    public class LinksConfigurations : IEntityTypeConfiguration<Links>
    {
        public void Configure(EntityTypeBuilder<Links> builder)
        {
            builder.HasKey(L => L.Id);
            builder.Property(L => L.Id).UseIdentityColumn(1,1);
            builder.HasOne(L => L.AppUser)
                   .WithMany(A => A.Links)
                   .HasForeignKey(L => L.AppUserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
