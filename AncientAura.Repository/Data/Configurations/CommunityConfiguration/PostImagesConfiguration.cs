using AncientAura.Core.Entities.Community;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations.CommunityConfiguration
{
    public class PostImagesConfiguration : IEntityTypeConfiguration<PostImages>
    {
        public void Configure(EntityTypeBuilder<PostImages> builder)
        {
            builder.HasKey(PI => PI.Id);
            builder.Property(PI => PI.Id).UseIdentityColumn(1,1);

            builder.HasOne(PI => PI.Post)
                   .WithMany(P => P.Images)
                   .HasForeignKey(PI => PI.PostId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
