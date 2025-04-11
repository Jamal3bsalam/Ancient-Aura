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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(A => A.Id);
            builder.HasMany(A => A.Posts)
                   .WithOne(P => P.User)
                   .HasForeignKey(P => P.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(A => A.Comments)
                   .WithOne(C => C.User)
                   .HasForeignKey(C => C.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(A => A.Reacts)
                   .WithOne(R => R.User)
                   .HasForeignKey(R => R.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(A => A.UserChats)
                   .WithOne(UC => UC.User)
                   .HasForeignKey(UC => UC.UserId)
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
