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
    public class PostsConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P => P.Id).UseIdentityColumn(1, 1);

            builder.HasMany(P => P.Comments)
                   .WithOne(C => C.Post)
                   .HasForeignKey(C => C.PostId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(P => P.Reacts)
                   .WithOne(R => R.Post)
                   .HasForeignKey(R => R.PostId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
