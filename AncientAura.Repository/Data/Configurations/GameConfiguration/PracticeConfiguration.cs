using AncientAura.Core.Entities.GameEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientAura.Repository.Data.Configurations.GameConfiguration
{
    public class PracticeConfiguration : IEntityTypeConfiguration<Practice>
    {
        public void Configure(EntityTypeBuilder<Practice> builder)
        {
            builder.HasKey(P => new { P.SymbolId, P.AppUserId });

            builder.HasOne(P => P.AppUser)
                   .WithMany(AU => AU.Practices)
                   .HasForeignKey(P => P.AppUserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
