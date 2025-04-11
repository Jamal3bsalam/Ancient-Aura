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
    public class VideosConfiguration : IEntityTypeConfiguration<Videos>
    {
        public void Configure(EntityTypeBuilder<Videos> builder)
        {
            builder.HasKey(V => V.Id);
            builder.Property(V => V.Id).UseIdentityColumn(1, 1);
        }
    }
}
