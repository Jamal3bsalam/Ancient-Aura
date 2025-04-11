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
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasKey(W => W.Id);
            builder.Property(W => W.Id).UseIdentityColumn(1, 1);

            builder.HasOne(W => W.AppUser)
                   .WithOne(A => A.WishList)
                   .HasForeignKey<WishList>(W => W.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);
                   
        }
    }
}
