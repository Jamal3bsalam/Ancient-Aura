using AncientAura.Core.Entities.WishLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations.WishListConfiguration
{
    public class ItemsConfiguration : IEntityTypeConfiguration<Items>
    {
        public void Configure(EntityTypeBuilder<Items> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn(1,1);

            builder.HasOne(I => I.WishList)
                   .WithMany(W => W.Items)
                   .HasForeignKey(I => I.WishListId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wi => wi.AncientSites)
           .WithMany(A => A.Items)
           .HasForeignKey(wi => wi.AncientSitesId)
           .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
