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
    public class DocumentriesConfiguration : IEntityTypeConfiguration<Documentries>
    {
        public void Configure(EntityTypeBuilder<Documentries> builder)
        {
            builder.HasKey(D => D.Id);
            builder.Property(D => D.Id).UseIdentityColumn(1,1);

            builder.HasMany(D => D.Reviews)
                   .WithOne(R => R.Documentries)
                   .HasForeignKey(R => R.DocumentriesId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
