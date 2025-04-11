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
    public class ArticlesConfiguration : IEntityTypeConfiguration<Articles>
    {
        public void Configure(EntityTypeBuilder<Articles> builder)
        {
            builder.HasKey(A => A.Id);
            builder.Property(A => A.Id).UseIdentityColumn(1, 1);

            builder.HasMany(A => A.Reviews)
                   .WithOne(R => R.Articles)
                   .HasForeignKey(R => R.ArticlesId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
