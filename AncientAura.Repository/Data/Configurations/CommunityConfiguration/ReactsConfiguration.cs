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
    public class ReactsConfiguration : IEntityTypeConfiguration<React>
    {
        public void Configure(EntityTypeBuilder<React> builder)
        {
            builder.HasKey(R => R.Id);
            builder.Property(R => R.Id).UseIdentityColumn(1,1);

            builder.Property(R => R.Type).HasConversion<string>();
        }
    }
}
