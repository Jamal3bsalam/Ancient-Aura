using AncientAura.Core.Entities.Community;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations.CommunityConfiguration
{
    public class CommentImageConfiguration : IEntityTypeConfiguration<CommentImages>
    {
        public void Configure(EntityTypeBuilder<CommentImages> builder)
        {
            builder.HasKey(CI => CI.Id);
            builder.Property(CI => CI.Id).UseIdentityColumn(1, 1);

            builder.HasOne(CI => CI.Comment)
                   .WithOne(C => C.CommentImages)
                   .HasForeignKey<CommentImages>(CI => CI.CommentId)
                   .OnDelete(DeleteBehavior.Cascade);
              
        }
    }
}
