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
    public class SymbolConfiguration : IEntityTypeConfiguration<Symbol>
    {
        public void Configure(EntityTypeBuilder<Symbol> builder)
        {
            builder.HasKey(S => S.Id);
            builder.Property(S => S.Id).UseIdentityColumn(1, 1);

            builder.HasMany(S => S.Practices)
                   .WithOne(P => P.Symbol)
                   .HasForeignKey(P => P.SymbolId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
