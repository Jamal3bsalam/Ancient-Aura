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
    public class CommentsConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(C => C.Id);
            builder.Property(C => C.Id).UseIdentityColumn(1,1);
            builder.HasMany(C => C.Reacts)
                   .WithOne(R => R.Comment)
                   .HasForeignKey(R => R.CommentId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(C => C.ParentComment)
                   .WithMany(C => C.Replies)
                   .HasForeignKey(C => C.ParentCommentId)
                   .OnDelete(DeleteBehavior.NoAction);

            
        }
    }
}
