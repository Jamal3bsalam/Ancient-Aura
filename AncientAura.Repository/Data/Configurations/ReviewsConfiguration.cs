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
    public class ReviewsConfiguration : IEntityTypeConfiguration<Reviews>
    {
        public void Configure(EntityTypeBuilder<Reviews> builder)
        {
            builder.HasKey(R => R.Id);
            builder.Property(R => R.Id).UseIdentityColumn(1, 1);

            builder.HasOne(R => R.AppUser)
                   .WithMany(A => A.Reviews)
                   .HasForeignKey(R => R.AppUserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
