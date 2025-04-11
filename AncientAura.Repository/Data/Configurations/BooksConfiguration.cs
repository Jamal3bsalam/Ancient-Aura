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
    public class BooksConfiguration : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.HasKey(B => B.Id);
            builder.Property(B => B.Id).UseIdentityColumn(1, 1);

            builder.HasMany(B => B.Reviews)
                   .WithOne(R => R.Books)
                   .HasForeignKey(R => R.BooksId)
                   .OnDelete(DeleteBehavior.SetNull);
                   
        }
    }
}
